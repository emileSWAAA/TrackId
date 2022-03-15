using System;
using System.Threading;
using System.Threading.Tasks;
using TrackId.Business.Dto;
using TrackId.Data.Wrappers;

namespace TrackId.Business.Services;

public interface ITrackService
{
    Task<PaginatedList<TrackDto>> GetPagedListAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);

    Task<TrackDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<TrackDto> AddAsync(TrackDto track, CancellationToken cancellationToken);

    Task<TrackDto> UpdateAsync(TrackDto track, CancellationToken cancellationToken);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}