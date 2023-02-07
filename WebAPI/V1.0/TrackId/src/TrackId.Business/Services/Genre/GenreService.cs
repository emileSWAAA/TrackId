using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using TrackId.Business.Dto;
using TrackId.Business.Dto.Genre;
using TrackId.Data.Entities;
using TrackId.Data.Interfaces;
using TrackId.Data.Repositories;
using TrackId.Data.Wrappers;

namespace TrackId.Business.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly ITrackRepository _trackRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GenreService> _logger;

        public GenreService(
            IGenreRepository genreRepository,
            IMapper mapper,
            ILogger<GenreService> logger,
            ITrackRepository trackRepository
         )
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
            _logger = logger;
            _trackRepository = trackRepository;
        }

        public async Task<GenreDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                _logger.LogInformation($"{nameof(GetByIdAsync)} has empty Guid.");
                return null;
            }

            var genre = await _genreRepository.GetByIdAsync(id, cancellationToken);
            if (genre is null)
            {
                return null;
            }

            return _mapper.Map<GenreDto>(genre);
        }

        public async Task<IPaginatedList<GenreDto>> GetPaginatedListAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            var genreList = await _genreRepository.GetPaginatedListByConditionAsync(null, x => x.OrderBy(o => o.Name), pageIndex, pageSize);
            if (genreList is null)
            {
                return null;
            }

            return _mapper.Map<PaginatedList<GenreDto>>(genreList);
        }

        public async Task<GenreDto> AddAsync(GenreDto genreDto, CancellationToken cancellationToken)
        {
            var genre = _mapper.Map<Genre>(genreDto);
            if (genre is null)
            {
                return null;
            }

            var result = await _genreRepository.AddAsync(genre, cancellationToken);
            if (result is null)
            {
                return null;
            }

            return _mapper.Map<GenreDto>(result);
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id.Equals(Guid.Empty))
            {
                return false;
            }

            return await _genreRepository.DeleteAsync(id, cancellationToken);
        }

        public async Task<GenreDto> UpdateAsync(GenreDto genreDto, CancellationToken cancellationToken)
        {
            if (genreDto is null)
            {
                return null;
            }

            var genre = _mapper.Map<Genre>(genreDto);
            if (genre is null)
            {
                return null;
            }

            if (await _genreRepository.UpdateAsync(genre, cancellationToken) is not Genre updatedResult)
            {
                return null;
            }

            return _mapper.Map<GenreDto>(updatedResult);
        }

        public async Task<GenreDto> GetGenreWithTracks(
            Guid id,
            int pageSize,
            int pageIndex,
            CancellationToken cancellationToken)
        {
            if (id.Equals(Guid.Empty))
            {
                return null;
            }

            if (await _genreRepository.GetByIdAsync(id, cancellationToken) is not Genre genre)
            {
                return null;
            }

            var tracks = await _trackRepository
                .GetPaginatedListByConditionAsync(
                    tr => tr.GenreId.HasValue && tr.GenreId.Equals(id),
                    o => o.OrderBy(tr => tr.CreateDateTime),
                    pageIndex, pageSize);

            var mappedGenre = _mapper.Map<GenreDto>(genre);
            if (tracks is not null)
            {
                mappedGenre.Tracks = _mapper.Map<PaginatedList<TrackDto>>(tracks);
            }

            return mappedGenre;
        }
    }
}
