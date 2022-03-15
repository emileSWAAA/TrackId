using TrackId.Contracts.Track;

namespace TrackId.Application.Queries.Track
{
    public class GetTrackQueryResult : BaseQueryResponse<GetTrackPaginatedResponse>
    {
        public GetTrackQueryResult(GetTrackPaginatedResponse result) : base(result) { }

        public GetTrackQueryResult(bool success, RequestErrorType errorType, string errorMessage)
            : base(success, errorType, errorMessage) { }

        public GetTrackQueryResult(RequestErrorType errorType, string errorMessage) : base(errorType, errorMessage) { }
    }
}
