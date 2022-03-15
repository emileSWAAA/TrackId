using AutoMapper;
using TrackId.Business.Dto;
using TrackId.Contracts.Models.User;
using TrackId.Data.Entities;

namespace TrackId.Infrastructure.Mapping
{
    public class RegistrationMapping : Profile
    {
        public RegistrationMapping()
        {
            CreateMap<RegistrationRequest, RegistrationDto>();
            CreateMap<RegistrationRequest, ApplicationUser>();
        }
    }
}
