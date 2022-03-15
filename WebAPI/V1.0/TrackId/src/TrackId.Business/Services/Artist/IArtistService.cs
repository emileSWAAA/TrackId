using System;
using System.Threading;
using System.Threading.Tasks;
using TrackId.Business.Dto;
using TrackId.Data.Wrappers;

namespace TrackId.Business.Services;

public interface IArtistService
{
    Task<ArtistDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ArtistDto> GetByNameAsync(string name, CancellationToken cancellationToken);

    Task<PaginatedList<ArtistDto>> GetPagedListAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);

    Task<ArtistDto> AddAsync(ArtistDto artist, CancellationToken cancellationToken);

    Task<bool> ExistsAsync(ArtistDto artist, CancellationToken cancellationToken);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);

    Task<ArtistDto> UpdateAsync(ArtistDto artist, CancellationToken cancellationToken);

}