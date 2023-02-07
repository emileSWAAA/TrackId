using System.Linq;
using AutoMapper;
using TrackId.Application.Commands.Track.Post;
using TrackId.Application.Commands.Track.Put;
using TrackId.Business.Dto;
using TrackId.Contracts.Track;
using TrackId.Contracts.Track.AddArtist;
using TrackId.Data.Entities;
using TrackId.Data.Wrappers;

namespace TrackId.Infrastructure.Mapping
{
    public class TrackMapping : Profile
    {
        public TrackMapping()
        {
            CreateMap<Track, TrackDto>()
                .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.Artists.Select(s => new ArtistDto
                {
                    Id = s.ArtistId
                })));

            CreateMap<TrackDto, Track>()
                .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.Artists.Select(s => new ArtistTrack
                    {
                        ArtistId = s.Id,
                        TrackId = src.Id
                    })));

            CreateMap<TrackDto, TrackViewModel>();
            CreateMap<TrackDto, TrackResult>()
                .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.Artists))
                .ForMember(dest => dest.Track, opt => opt.MapFrom(src => src));

            CreateMap<TrackDto, GetByIdTrackResponse>();
            CreateMap<PostTrackCommand, TrackDto>()
                .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.Artists.Select(art => new ArtistDto()
                {
                    Id = art
                })));

            CreateMap<TrackDto, PostTrackResponse>()
                .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.Artists.Select(art => art.Id)));

            CreateMap<PutTrackCommand, TrackDto>()
                .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.Artists.Select(art => new ArtistDto()
                {
                    Id = art
                })));

            CreateMap<TrackDto, PutTrackResponse>()
                .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.Artists.Select(art => art.Id)));

            CreateMap<TrackDto, AddArtistsResponse>()
                .ForMember(dest => dest.TrackId, opt => opt.MapFrom(src => src.Id));

            CreateMap<PaginatedList<Track>, PaginatedList<TrackDto>>()
                .ForCtorParam("items", opt => opt.MapFrom(src => src.Items))
                .ForCtorParam("totalCount", opt => opt.MapFrom(src => src.TotalCount))
                .ForCtorParam("pageIndex", opt => opt.MapFrom(src => src.PageIndex))
                .ForCtorParam("pageSize", opt => opt.MapFrom(src => src.PageSize));

            CreateMap<PaginatedList<TrackDto>, PaginatedList<TrackResult>>()
                .ForCtorParam("items", opt => opt.MapFrom(src => src.Items))
                .ForCtorParam("totalCount", opt => opt.MapFrom(src => src.TotalCount))
                .ForCtorParam("pageIndex", opt => opt.MapFrom(src => src.PageIndex))
                .ForCtorParam("pageSize", opt => opt.MapFrom(src => src.PageSize));
        }
    }
}
