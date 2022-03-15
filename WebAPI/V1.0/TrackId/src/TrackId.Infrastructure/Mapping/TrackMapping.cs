using System.Linq;
using AutoMapper;
using TrackId.Application.Commands.Track.Post;
using TrackId.Application.Commands.Track.Put;
using TrackId.Business.Dto;
using TrackId.Contracts.Track;
using TrackId.Data.Entities;
using TrackId.Data.Wrappers;

namespace TrackId.Infrastructure.Mapping
{
    public class TrackMapping : Profile
    {
        public TrackMapping()
        {
            CreateMap<Track, TrackDto>()
                .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.Artists.Select(s => s.ArtistId)));

            CreateMap<TrackDto, Track>()
                .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.Artists.Select(s => new ArtistTrack()
                    {
                        ArtistId = s,
                        TrackId = src.Id
                    })));

            CreateMap<TrackDto, TrackViewModel>();

            CreateMap<TrackDto, GetByIdTrackResponse>();
            CreateMap<PostTrackCommand, TrackDto>();
            CreateMap<TrackDto, PostTrackResponse>()
                .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.Artists.Select(art => art)));

            CreateMap<PutTrackCommand, TrackDto>();

            CreateMap<TrackDto, PutTrackResponse>()
                .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.Artists.Select(art => art)));

            CreateMap<PaginatedList<Track>, PaginatedList<TrackDto>>()
                .ForCtorParam("items", opt => opt.MapFrom(src => src.Items))
                .ForCtorParam("totalCount", opt => opt.MapFrom(src => src.TotalCount))
                .ForCtorParam("pageIndex", opt => opt.MapFrom(src => src.PageIndex))
                .ForCtorParam("pageSize", opt => opt.MapFrom(src => src.PageSize));

            CreateMap<PaginatedList<TrackDto>, PaginatedList<TrackViewModel>>()
                .ForCtorParam("items", opt => opt.MapFrom(src => src.Items))
                .ForCtorParam("totalCount", opt => opt.MapFrom(src => src.TotalCount))
                .ForCtorParam("pageIndex", opt => opt.MapFrom(src => src.PageIndex))
                .ForCtorParam("pageSize", opt => opt.MapFrom(src => src.PageSize));
        }
    }
}
