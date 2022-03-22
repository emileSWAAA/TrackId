using System.Collections.Generic;
using TrackId.Contracts.Track;
using TrackId.Data.Wrappers;

namespace TrackId.Contracts.Artist.GetPaginated
{
    public class GetArtistPaginatedResponse : IResponseContract
    {
        public PaginatedList<ArtistResult> Result { get; set; }
    }

    public class ArtistResult
    {
        public ArtistViewModel Artist { get; set; }

        public IEnumerable<TrackViewModel> Tracks { get; set; }
    }
}
