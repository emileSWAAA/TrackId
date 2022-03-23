using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackId.Common.Helpers;
using TrackId.Data.Entities;
using TrackId.Data.Interfaces;
using TrackId.Data.Wrappers;

namespace TrackId.Data.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<GenreRepository> _logger;
        private readonly IDateTimeProvider _dateTimeProvider;

        public GenreRepository(
            ApplicationDbContext dbContext,
            ILogger<GenreRepository> logger,
            IDateTimeProvider dateTimeProvider)
        {
            _dbContext = dbContext;
            _logger = logger;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Genre> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
            await _dbContext.Genres
                .Include(genre => genre.ParentGenre)
                .AsNoTracking()
                .FirstOrDefaultAsync(genre => genre.Id.Equals(id) && !genre.IsDeleted, cancellationToken);

        public async Task<Genre> AddAsync(Genre genre, CancellationToken cancellationToken)
        {
            if (genre.Id.Equals(Guid.Empty))
            {
                throw new ArgumentException("Can not add Genre with empty guid.", nameof(genre));
            }

            genre.CreateDateTime = DateTime.UtcNow;
            genre.Id = Guid.NewGuid();
            genre.ParentGenre = null;

            var result = await _dbContext.Genres.AddAsync(genre, cancellationToken);
            if (await _dbContext.SaveChangesAsync(cancellationToken) > 0)
            {
                return result.Entity;
            }

            return null;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity is null)
            {
                throw new InvalidOperationException($"Genre with Id: '{id}' not found in database. Cannot delete.");
            }

            _dbContext.Genres.Remove(entity);
            return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> SoftDeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity == null)
            {
                return false;
            }

            entity.IsDeleted = true;
            entity.DeleteDateTime = _dateTimeProvider.UtcNow;

            return await UpdateAsync(entity, cancellationToken) != null;
        }

        public async Task<Genre> UpdateAsync(Genre genre, CancellationToken cancellationToken)
        {
            if (genre is null)
            {
                return null;
            }

            if (genre.Id.Equals(Guid.Empty))
            {
                throw new ArgumentException("Can not add Genre with empty guid.", nameof(genre));
            }

            if (await GetByIdAsync(genre.Id, cancellationToken) is not Genre existingGenre)
            {
                return null;
            }

            _dbContext.Entry(existingGenre).CurrentValues.SetValues(genre);
            _dbContext.Entry(existingGenre).State = EntityState.Modified;

            if (await _dbContext.SaveChangesAsync(cancellationToken) <= 0)
            {
                return null;
            }

            return existingGenre;
        }

        public async Task<Genre> GetSingleByConditionAsync(Expression<Func<Genre, bool>> predicate,
            CancellationToken cancellationToken)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return await _dbContext.Genres
                .Include(art => art.ParentGenre)
                .AsNoTracking()
                .AsQueryable()
                .FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<IPaginatedList<Genre>> GetPaginatedListByConditionAsync(
            Expression<Func<Genre, bool>> predicate = null,
            Func<IQueryable<Genre>, IOrderedQueryable<Genre>> orderBy = null,
            int pageIndex = 0,
            int pageSize = 20)
        {
            pageSize = PaginatedListHelper.ParsePageSize(pageSize);
            pageIndex = PaginatedListHelper.ParsePageIndex(pageIndex);

            var query = _dbContext.Genres
                .Include(art => art.ParentGenre)
                .AsNoTracking()
                .AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            var totalCount = await query
                .AsNoTracking()
                .CountAsync(genre => !genre.IsDeleted);

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            var skip = PaginatedListHelper.ParseSkip(pageIndex, pageSize);
            if (skip > 0)
            {
                query = query.Skip(skip);
            }

            query = query.Take(pageSize);
            return new PaginatedList<Genre>(
                totalCount,
                pageIndex,
                pageSize,
                query);
        }
    }
}
