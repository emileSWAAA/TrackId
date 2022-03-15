using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using TrackId.Business.Dto;
using TrackId.Data.Entities;
using TrackId.Data.Repositories;
using TrackId.Data.Wrappers;

namespace TrackId.Business.Services
{
    public class TrackService : ITrackService
    {
        private readonly ITrackRepository _trackRepository;
        private readonly ILogger<TrackService> _logger;
        private readonly IMapper _mapper;

        public TrackService(
            ITrackRepository trackRepository,
            ILogger<TrackService> logger,
            IMapper mapper)
        {
            _trackRepository = trackRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PaginatedList<TrackDto>> GetPagedListAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            if (pageIndex < 0)
            {
                throw new ArgumentException($"{nameof(pageIndex)} must be a non-negative number.", nameof(pageIndex));
            }

            if (pageSize < 1)
            {
                throw new ArgumentException($"{nameof(pageSize)} must be a higher than zero.", nameof(pageSize));
            }

            var tracks = await _trackRepository
                .GetPaginatedListByConditionAsync(predicate: null, x => x.OrderBy(o => o.CreateDateTime), pageIndex, pageSize);
            return _mapper.Map<PaginatedList<TrackDto>>(tracks);
        }

        public async Task<TrackDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                _logger.LogInformation($"{nameof(GetByIdAsync)} has empty Guid.");
                return null;
            }

            var track = await _trackRepository.GetByIdAsync(id, cancellationToken);
            if (track is null)
            {
                return null;
            }

            return _mapper.Map<TrackDto>(track);
        }

        public async Task<TrackDto> AddAsync(TrackDto track, CancellationToken cancellationToken)
        {
            if (track is null)
            {
                throw new ArgumentNullException(nameof(track));
            }

            var trackEnt = _mapper.Map<Track>(track);
            if (trackEnt is null)
            {
                return null;
            }

            if (await _trackRepository.AddAsync(trackEnt, cancellationToken) is not Track result)
            {
                return null;
            }

            var res = _mapper.Map<TrackDto>(result);

            return res;
        }

        public async Task<TrackDto> UpdateAsync(TrackDto trackDto, CancellationToken cancellationToken)
        {
            if (trackDto is null)
            {
                return null;
            }

            var trackEnt = _mapper.Map<Track>(trackDto);
            if (trackEnt is null)
            {
                return null;
            }

            if (await _trackRepository.UpdateAsync(trackEnt, cancellationToken) is not Track updatedResult)
            {
                return null;
            }

            return _mapper.Map<TrackDto>(updatedResult);
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            if (!await _trackRepository.ExistsAsync(x => x.Id.Equals(id), cancellationToken))
            {
                return false;
            }

            await _trackRepository.SoftDeleteAsync(id, cancellationToken);
            return true;
        }

        public async Task<TrackDto> AddArtistsAsync(Guid trackId, IEnumerable<Guid> artistIds, CancellationToken cancellationToken)
        {
            if (await _trackRepository.AddArtistsAsync(trackId, artistIds, cancellationToken) is not Track track)
            {
                return null;
            }

            return _mapper.Map<TrackDto>(track);
        }
    }
}
