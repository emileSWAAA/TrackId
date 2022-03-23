using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TrackId.Data.Entities;
using TrackId.Data.Interfaces;

namespace TrackId.Data.Repositories;

public interface IArtistRepository
{
    Task<Artist> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);

    Task<Artist> AddAsync(Artist entity, CancellationToken cancellationToken);

    Task<bool> SoftDeleteAsync(Guid id, CancellationToken cancellationToken);

    Task<Artist> UpdateAsync(Artist entity, CancellationToken cancellationToken);

    Task<Artist> GetSingleByConditionAsync(Expression<Func<Artist, bool>> predicate, CancellationToken cancellationToken);

    Task<bool> ExistsAsync(Expression<Func<Artist, bool>> predicate, CancellationToken cancellationToken);

    Task<IPaginatedList<Artist>> GetPaginatedListByConditionAsync(
        Expression<Func<Artist, bool>> predicate = null,
        Func<IQueryable<Artist>, IOrderedQueryable<Artist>> orderBy = null,
        int pageIndex = 0,
        int pageSize = 20);

}