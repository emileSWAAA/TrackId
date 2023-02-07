using AutoMapper;
using TrackId.Business.Dto;
using TrackId.Contracts.Models.User;
using TrackId.Data.Entities;

namespace TrackId.WebAPI.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<UserDto, LoginResponse>()
                .ForMember(dest => dest.Token, opt => opt.Ignore());

            CreateMap<ApplicationUser, UserDto>();
            CreateMap<UserDto, ApplicationUser>();
        }
    }
}
