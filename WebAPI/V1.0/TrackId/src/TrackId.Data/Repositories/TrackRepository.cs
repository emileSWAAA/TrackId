using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackId.Common.Constants;
using TrackId.Data.Entities;
using TrackId.Data.Interfaces;
using TrackId.Data.Wrappers;

namespace TrackId.Data.Repositories
{
    public class TrackRepository : ITrackRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ILogger<TrackRepository> _logger;

        public TrackRepository(ApplicationDbContext dbContext,
            ILogger<TrackRepository> logger,
            IDateTimeProvider dateTimeProvider)
        {
            _dbContext = dbContext;
            _logger = logger;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<IPaginatedList<Track>> GetPaginatedListByConditionAsync(
            Expression<Func<Track, bool>> predicate = null, Func<IQueryable<Track>,
                IOrderedQueryable<Track>> orderBy = null,
            int pageIndex = 0,
            int pageSize = 20)
        {
            if (pageSize > ApplicationConstants.MaxPageSize)
            {
                pageSize = ApplicationConstants.MaxPageSize;
            }

            var query = _dbContext.Tracks
                .Include(tr => tr.Artists)
                .ThenInclude(art => art.Artist)
                .Include(tr => tr.Genre)
                .AsNoTracking()
                .Where(tr => tr.IsDeleted == false)
                .AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            var totalCount = await query
                .AsNoTracking()
                .CountAsync();

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            var skip = (pageIndex) * pageSize;
            if (skip < 0)
            {
                skip = 0;
            }

            if (skip > 0)
            {
                query = query.Skip(skip);
            }

            query = query.Take(pageSize);
            return new PaginatedList<Track>(
                totalCount,
                pageIndex,
                pageSize,
                await query.ToListAsync());
        }

        public async Task<Track> AddAsync(Track entity, CancellationToken cancellationToken)
        {
            var existingTrack = await GetByIdAsync(entity.Id, cancellationToken);
            if (existingTrack is not null)
            {
                return null;
            }

            entity.Id = Guid.NewGuid();
            entity.CreateDateTime = _dateTimeProvider.UtcNow;

            foreach (var artist in entity.Artists)
            {
                artist.TrackId = entity.Id;
                artist.CreateDateTime = _dateTimeProvider.UtcNow;
                artist.IsDeleted = false;
            }

            var result = await _dbContext.Tracks.AddAsync(entity, cancellationToken);
            if (await _dbContext.SaveChangesAsync(cancellationToken) > 0)
            {
                return result.Entity;
            }

            return null;
        }

        public async Task<Track> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Tracks
                .Include(tr => tr.Artists)
                .ThenInclude(art => art.Artist)
                .Include(tr => tr.Genre)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity is null)
            {
                throw new InvalidOperationException($"Object with Id: '{id}' not found in database. Cannot delete.");
            }

            _dbContext.Tracks.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Track> SoftDeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity == null)
            {
                return null;
            }

            entity.IsDeleted = true;
            entity.DeleteDateTime = _dateTimeProvider.UtcNow;

            return await UpdateAsync(entity, cancellationToken);
        }

        public async Task<Track> UpdateAsync(Track entity, CancellationToken cancellationToken)
        {
            if (await GetByIdAsync(entity.Id, cancellationToken) is not Track existingTrack)
            {
                return null;
            }

            UpdateArtists(existingTrack, entity.Artists);
            _dbContext.Entry(existingTrack).CurrentValues.SetValues(entity);
            _dbContext.Entry(existingTrack).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return existingTrack;
        }

        public async Task<bool> ExistsAsync(Expression<Func<Track, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbContext.Tracks
                .AsNoTracking()
                .AnyAsync(predicate, cancellationToken);
        }

        public async Task<Track> AddArtistsAsync(Guid trackId, IEnumerable<Guid> artistIds,
            CancellationToken cancellationToken)
        {
            if (await GetByIdAsync(trackId, cancellationToken) is not Track existingTrack)
            {
                return null;
            }

            foreach (var artist in artistIds)
            {
                if (await _dbContext.ArtistTracks.AnyAsync(art =>
                        art.TrackId.Equals(existingTrack.Id) && art.ArtistId.Equals(artist) && !art.IsDeleted,
                        cancellationToken))
                {
                    continue;
                }

                await _dbContext.ArtistTracks.AddAsync(CreateArtistTrack(artist, existingTrack.Id), cancellationToken);
            }

            if (await _dbContext.SaveChangesAsync(cancellationToken) <= 0)
            {
                return null;
            }

            return existingTrack;
        }

        private void UpdateArtists(Track track, IEnumerable<ArtistTrack> artists)
        {
            foreach (var artist in track.Artists)
            {
                if (!track.Artists.Any(art => art.ArtistId.Equals(artist.ArtistId) &&
                                              art.TrackId.Equals(track.Id)))
                {
                    _dbContext.Entry(artist).State = EntityState.Deleted;
                }
            }

            foreach (var artist in artists)
            {
                var existingArtist = track.Artists.SingleOrDefault(art => art.ArtistId.Equals(artist.ArtistId) &&
                                                                            art.TrackId.Equals(track.Id));
                if (existingArtist is not null)
                {
                    _dbContext.Entry(existingArtist).State = EntityState.Modified;
                    continue;
                }

                existingArtist = CreateArtistTrack(artist.ArtistId, track.Id);
                _dbContext.Entry(existingArtist).State = EntityState.Added;
            }
        }

        private static ArtistTrack CreateArtistTrack(Guid artistId, Guid trackId)
        {
            return new ArtistTrack()
            {
                ArtistId = artistId,
                Id = Guid.NewGuid(),
                CreateDateTime = DateTime.UtcNow,
                TrackId = trackId,
                IsDeleted = false
            };
        }
    }
}
