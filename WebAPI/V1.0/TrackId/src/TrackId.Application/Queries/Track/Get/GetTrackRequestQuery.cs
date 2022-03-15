using MediatR;

namespace TrackId.Application.Queries.Track
{
    public class GetTrackRequestQuery : IRequest<GetTrackQueryResult>
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
