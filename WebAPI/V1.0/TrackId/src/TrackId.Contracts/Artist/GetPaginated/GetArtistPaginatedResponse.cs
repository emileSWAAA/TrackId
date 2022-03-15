using TrackId.Data.Wrappers;

namespace TrackId.Contracts.Artist.GetPaginated
{
    public record GetArtistPaginatedResponse : IResponseContract
    {
        public PaginatedList<ArtistViewModel> Result { get; set; }
    }
}
