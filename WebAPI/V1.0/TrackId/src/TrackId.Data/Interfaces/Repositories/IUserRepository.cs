using System;
using System.Threading;
using System.Threading.Tasks;
using TrackId.Data.Entities;

namespace TrackId.Data.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetByEmailAsync(string email, CancellationToken cancellationToken);

        Task<ApplicationUser> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}