using System.Linq;
using AutoMapper;
using TrackId.Business.Dto;
using TrackId.Contracts.Artist;
using TrackId.Contracts.Artist.GetPaginated;
using TrackId.Contracts.Artist.Post;
using TrackId.Contracts.Track;
using TrackId.Data.Entities;
using TrackId.Data.Wrappers;

namespace TrackId.Infrastructure.Mapping
{
    public class ArtistMapping : Profile
    {
        public ArtistMapping()
        {
            CreateMap<Artist, ArtistDto>()
                .ForMember(art => art.Tracks, opt => opt.MapFrom(src => src.Tracks.Select(s => s.Track)));

            CreateMap<ArtistDto, Artist>()
                .ForMember(art => art.Tracks, opt => opt.MapFrom(src => src.Tracks.Select(s => s)));

            CreateMap<ArtistDto, ArtistViewModel>();
            CreateMap<ArtistDto, ArtistResult>()
                .ForMember(dest => dest.Tracks, opt => opt.MapFrom(src => src.Tracks))
                .ForMember(dest => dest.Artist, opt => opt.MapFrom(src => src));

            CreateMap<ArtistDto, PostArtistResponse>();
            CreateMap<PostArtistRequest, ArtistDto>()
                .ForMember(dest => dest.Tracks, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<ArtistDto, GetByIdArtistResponse>();

            CreateMap<PaginatedList<Artist>, PaginatedList<ArtistDto>>()
                .ForCtorParam("items", opt => opt.MapFrom(src => src.Items))
                .ForCtorParam("totalCount", opt => opt.MapFrom(src => src.TotalCount))
                .ForCtorParam("pageIndex", opt => opt.MapFrom(src => src.PageIndex))
                .ForCtorParam("pageSize", opt => opt.MapFrom(src => src.PageSize));

            CreateMap<PaginatedList<ArtistDto>, PaginatedList<ArtistResult>>()
                .ForCtorParam("items", opt => opt.MapFrom(src => src.Items))
                .ForCtorParam("totalCount", opt => opt.MapFrom(src => src.TotalCount))
                .ForCtorParam("pageIndex", opt => opt.MapFrom(src => src.PageIndex))
                .ForCtorParam("pageSize", opt => opt.MapFrom(src => src.PageSize));
        }
    }
}
