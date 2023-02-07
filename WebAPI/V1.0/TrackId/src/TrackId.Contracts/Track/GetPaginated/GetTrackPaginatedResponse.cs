using System.Collections.Generic;
using TrackId.Contracts.Artist;
using TrackId.Data.Wrappers;

namespace TrackId.Contracts.Track
{
    public class GetTrackPaginatedResponse : IResponseContract
    {
        public PaginatedList<TrackResult> Result { get; set; }
    }

    public class TrackResult
    {
        public TrackViewModel Track { get; set; }

        public IEnumerable<ArtistViewModel> Artists { get; set; }
    }
}
