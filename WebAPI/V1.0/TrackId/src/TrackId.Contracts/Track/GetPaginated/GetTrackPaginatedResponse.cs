using TrackId.Data.Wrappers;

namespace TrackId.Contracts.Track
{
    public class GetTrackPaginatedResponse : IResponseContract
    {
        public PaginatedList<TrackViewModel> Result { get; set; }
    }
}
