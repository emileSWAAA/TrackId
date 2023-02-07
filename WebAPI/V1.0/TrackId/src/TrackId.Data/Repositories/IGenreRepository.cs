using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TrackId.Data.Entities;
using TrackId.Data.Interfaces;

namespace TrackId.Data.Repositories;

public interface IGenreRepository
{
    Task<Genre> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<Genre> AddAsync(Genre genre, CancellationToken cancellationToken);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);

    Task<bool> SoftDeleteAsync(Guid id, CancellationToken cancellationToken);

    Task<Genre> UpdateAsync(Genre genre, CancellationToken cancellationToken);

    Task<Genre> GetSingleByConditionAsync(Expression<Func<Genre, bool>> predicate, CancellationToken cancellationToken);

    Task<IPaginatedList<Genre>> GetPaginatedListByConditionAsync(
        Expression<Func<Genre, bool>> predicate = null,
        Func<IQueryable<Genre>, IOrderedQueryable<Genre>> orderBy = null,
        int pageIndex = 0,
        int pageSize = 20);
}