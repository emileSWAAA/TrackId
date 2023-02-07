using System;
using System.Threading;
using System.Threading.Tasks;
using TrackId.Business.Dto.Genre;
using TrackId.Data.Interfaces;

namespace TrackId.Business.Services
{
    public interface IGenreService
    {
        Task<GenreDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<IPaginatedList<GenreDto>> GetPaginatedListAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);

        Task<GenreDto> AddAsync(GenreDto genreDto, CancellationToken cancellationToken);

        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);

        Task<GenreDto> UpdateAsync(GenreDto genreDto, CancellationToken cancellationToken);

        Task<GenreDto> GetGenreWithTracks(
            Guid id,
            int pageSize,
            int pageIndex,
            CancellationToken cancellationToken);
    }
}
