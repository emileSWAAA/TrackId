using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackId.Common.Constants;
using TrackId.Common.Helpers;
using TrackId.Data.Entities;
using TrackId.Data.Interfaces;
using TrackId.Data.Wrappers;

namespace TrackId.Data.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ArtistRepository> _logger;
        private readonly IDateTimeProvider _dateTimeProvider;

        public ArtistRepository(ApplicationDbContext dbContext, ILogger<ArtistRepository> logger, IDateTimeProvider dateTimeProvider)
        {
            _dbContext = dbContext;
            _logger = logger;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Artist> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Artists
                .Include(art => art.Tracks)
                .ThenInclude(tr => tr.Track)
                .AsNoTracking()
                .FirstOrDefaultAsync(art => art.Id.Equals(id) && art.IsDeleted == false, cancellationToken);

            if (id.Equals(ArtistConstants.TbaGuid))
            {
                result.Tracks = null;
            }

            return result;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id.Equals(ArtistConstants.TbaGuid))
            {
                throw new ArgumentException("Can not delete TBA artist", nameof(id));
            }

            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity is null)
            {
                throw new InvalidOperationException($"Artist with Id: '{id}' not found in database. Cannot delete.");
            }

            _dbContext.Artists.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Artist> AddAsync(Artist entity, CancellationToken cancellationToken)
        {
            if (entity.Id.Equals(ArtistConstants.TbaGuid) ||
                entity.Name.Equals(ArtistConstants.TbaName, StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Can not add TBA artist", nameof(entity));
            }

            entity.CreateDateTime = DateTime.UtcNow;
            entity.Id = Guid.NewGuid();

            var result = await _dbContext.Artists.AddAsync(entity, cancellationToken);
            if (await _dbContext.SaveChangesAsync(cancellationToken) > 0)
            {
                return result.Entity;
            }

            return null;
        }

        public async Task<bool> SoftDeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id.Equals(ArtistConstants.TbaGuid))
            {
                throw new ArgumentException("Can not delete TBA artist", nameof(id));
            }

            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity == null)
            {
                return false;
            }

            entity.IsDeleted = true;
            entity.DeleteDateTime = _dateTimeProvider.UtcNow;

            return await UpdateAsync(entity, cancellationToken) != null;
        }

        public async Task<Artist> UpdateAsync(Artist entity, CancellationToken cancellationToken)
        {
            if (entity is null)
            {
                return null;
            }

            if (entity.Id.Equals(ArtistConstants.TbaGuid))
            {
                throw new ArgumentException("Can not update TBA artist", nameof(entity));
            }

            if (await GetByIdAsync(entity.Id, cancellationToken) is not Artist existingArtist)
            {
                return null;
            }

            _dbContext.Entry(existingArtist).CurrentValues.SetValues(entity);
            _dbContext.Entry(existingArtist).State = EntityState.Modified;

            if (await _dbContext.SaveChangesAsync(cancellationToken) <= 0)
            {
                return null;
            }

            return existingArtist;
        }

        public async Task<Artist> GetSingleByConditionAsync(Expression<Func<Artist, bool>> predicate, CancellationToken cancellationToken)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return await _dbContext.Artists
                .Include(art => art.Tracks)
                .ThenInclude(tr => tr.Track)
                .AsNoTracking()
                .AsQueryable()
                .FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<bool> ExistsAsync(Expression<Func<Artist, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbContext.Artists
                .AsNoTracking()
                .AnyAsync(predicate, cancellationToken);
        }

        public async Task<IPaginatedList<Artist>> GetPaginatedListByConditionAsync(
            Expression<Func<Artist, bool>> predicate = null,
            Func<IQueryable<Artist>, IOrderedQueryable<Artist>> orderBy = null,
            int pageIndex = 0,
            int pageSize = 20)
        {
            pageSize = PaginatedListHelper.ParsePageSize(pageSize);
            pageIndex = PaginatedListHelper.ParsePageIndex(pageIndex);

            var query = _dbContext.Artists
                .Include(art => art.Tracks)
                .ThenInclude(tr => tr.Track)
                .AsNoTracking()
                .AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            var totalCount = await query
                .AsNoTracking()
                .CountAsync(art => !art.IsDeleted);

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            var skip = PaginatedListHelper.ParseSkip(pageIndex, pageSize);
            if (skip < 0)
            {
                skip = 0;
            }

            if (skip > 0)
            {
                query = query.Skip(skip);
            }

            query = query.Take(pageSize);
            return new PaginatedList<Artist>(
                totalCount,
                pageIndex,
                pageSize,
                query);
        }
    }
}
