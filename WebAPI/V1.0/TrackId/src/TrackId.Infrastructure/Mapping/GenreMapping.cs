using AutoMapper;
using TrackId.Business.Dto.Genre;
using TrackId.Contracts.Genre;
using TrackId.Data.Entities;
using TrackId.Data.Wrappers;

namespace TrackId.Infrastructure.Mapping
{
    public class GenreMapping : Profile
    {
        public GenreMapping()
        {
            CreateMap<Genre, GenreDto>();
            CreateMap<GenreDto, Genre>();

            CreateMap<GenreDto, GenreViewModel>()
                .ForMember(dest => dest.ParentGenre, opt => opt.MapFrom(src => new ParentGenreViewModel
                {
                    Id = src.ParentGenre.Id,
                    Name = src.ParentGenre.Name
                }))
                .ForMember(dest => dest.Tracks, opt => opt.MapFrom(src => src.Tracks.Items));

            CreateMap<GenreViewModel, GenreDto>();

            CreateMap<PaginatedList<Genre>, PaginatedList<GenreDto>>()
                .ForCtorParam("items", opt => opt.MapFrom(src => src.Items))
                .ForCtorParam("totalCount", opt => opt.MapFrom(src => src.TotalCount))
                .ForCtorParam("pageIndex", opt => opt.MapFrom(src => src.PageIndex))
                .ForCtorParam("pageSize", opt => opt.MapFrom(src => src.PageSize));

            CreateMap<PaginatedList<GenreDto>, PaginatedList<GenreViewModel>>()
                .ForCtorParam("items", opt => opt.MapFrom(src => src.Items))
                .ForCtorParam("totalCount", opt => opt.MapFrom(src => src.TotalCount))
                .ForCtorParam("pageIndex", opt => opt.MapFrom(src => src.PageIndex))
                .ForCtorParam("pageSize", opt => opt.MapFrom(src => src.PageSize));

        }
    }
}
