using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using TrackId.Business.Dto;
using TrackId.Common.Constants;
using TrackId.Data.Entities;
using TrackId.Data.Interfaces;
using TrackId.Data.Repositories;
using TrackId.Data.Wrappers;

namespace TrackId.Business.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;
        private readonly ILogger<ArtistService> _logger;
        private readonly IMapper _mapper;

        public ArtistService(
            IArtistRepository artistRepository,
            ILogger<ArtistService> logger,
            IMapper mapper)
        {
            _artistRepository = artistRepository;
            _logger = logger;
            _mapper = mapper;
        }


        public async Task<ArtistDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                _logger.LogInformation($"{nameof(GetByIdAsync)} has empty Guid.");
                return null;
            }

            var artist = await _artistRepository.GetByIdAsync(id, cancellationToken);
            if (artist is null)
            {
                return null;
            }

            return _mapper.Map<ArtistDto>(artist);
        }

        public async Task<ArtistDto> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var artist = await _artistRepository.GetSingleByConditionAsync(art => art.Name.Equals(name), cancellationToken);
            if (artist is null)
            {
                return null;
            }

            return _mapper.Map<ArtistDto>(artist);
        }

        public async Task<IPaginatedList<ArtistDto>> GetPagedListAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            var artistList = await _artistRepository.GetPaginatedListByConditionAsync(null, x => x.OrderBy(o => o.CreateDateTime), pageIndex, pageSize);
            if (artistList is null)
            {
                return null;
            }

            return _mapper.Map<PaginatedList<ArtistDto>>(artistList);
        }

        public async Task<ArtistDto> AddAsync(ArtistDto artist, CancellationToken cancellationToken)
        {
            var artistEnt = _mapper.Map<Artist>(artist);
            if (artistEnt is null)
            {
                return null;
            }

            var result = await _artistRepository.AddAsync(artistEnt, cancellationToken);
            if (result is null)
            {
                return null;
            }

            return _mapper.Map<ArtistDto>(result);
        }

        public async Task<bool> ExistsAsync(ArtistDto artist, CancellationToken cancellationToken)
        {
            if (artist.Id.Equals(ArtistConstants.TbaGuid) ||
                artist.Name.Equals(ArtistConstants.TbaName, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (await _artistRepository.GetByIdAsync(artist.Id, cancellationToken) is not null)
            {
                return true;
            }

            if (await _artistRepository.GetSingleByConditionAsync(art => art.Name.Equals(artist.Name), cancellationToken) is not null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id.Equals(Guid.Empty))
            {
                return false;
            }

            if (!await _artistRepository.ExistsAsync(x => x.Id.Equals(id), cancellationToken))
            {
                return false;
            }

            await _artistRepository.DeleteAsync(id, cancellationToken);
            return true;
        }

        public async Task<ArtistDto> UpdateAsync(ArtistDto artist, CancellationToken cancellationToken)
        {
            if (artist is null)
            {
                return null;
            }

            var artistEnt = _mapper.Map<Artist>(artist);
            if (artistEnt is null)
            {
                return null;
            }

            if (await _artistRepository.UpdateAsync(artistEnt, cancellationToken) is not Artist updatedResult)
            {
                return null;
            }

            return _mapper.Map<ArtistDto>(updatedResult);
        }
    }
}
