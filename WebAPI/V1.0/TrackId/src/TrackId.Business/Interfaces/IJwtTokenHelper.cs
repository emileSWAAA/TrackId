using System.Threading.Tasks;
using TrackId.Business.Dto;

namespace TrackId.Business.Interfaces
{
    public interface IJwtTokenHelper
    {
        Task<string> CreateToken(UserDto user);
    }
}