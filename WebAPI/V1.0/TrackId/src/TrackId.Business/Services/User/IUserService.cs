using System;
using System.Threading;
using System.Threading.Tasks;
using TrackId.Business.Dto;

namespace TrackId.Business.User
{
    public interface IUserService
    {
        Task<UserDto> GetById(Guid id, CancellationToken cancellationToken);

        Task<UserDto> GetByEmail(string email);
    }
}