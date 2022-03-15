using TrackId.Contracts.Track;

namespace TrackId.Application.Queries.Track
{
    public class GetByIdTrackQueryResult : BaseQueryResponse<GetByIdTrackResponse>
    {
        public GetByIdTrackQueryResult(GetByIdTrackResponse result)
            : base(result)
        {
        }

        public GetByIdTrackQueryResult(bool success, RequestErrorType errorType, string errorMessage)
            : base(success, errorType, errorMessage)
        {
        }
    }
}
