using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TrackId.Data.Entities;
using TrackId.Data.Interfaces;

namespace TrackId.Data.Repositories;

public interface ITrackRepository
{
    Task<IPaginatedList<Track>> GetPaginatedListByConditionAsync(Expression<Func<Track, bool>> predicate = null,
        Func<IQueryable<Track>, IOrderedQueryable<Track>> orderBy = null,
        int pageIndex = 0,
        int pageSize = 20);

    Task<Track> AddAsync(Track entity, CancellationToken cancellationToken);

    Task<Track> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);

    Task<Track> SoftDeleteAsync(Guid id, CancellationToken cancellationToken);

    Task<Track> UpdateAsync(Track entity, CancellationToken cancellationToken);

    Task<bool> ExistsAsync(Expression<Func<Track, bool>> predicate, CancellationToken cancellationToken);
}