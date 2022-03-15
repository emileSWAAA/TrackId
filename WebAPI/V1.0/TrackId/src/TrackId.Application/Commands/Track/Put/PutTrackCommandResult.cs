using TrackId.Application.Queries;
using TrackId.Contracts.Track;

namespace TrackId.Application.Commands.Track.Put
{
    public class PutTrackCommandResult : BaseQueryResponse<PutTrackResponse>
    {
        public PutTrackCommandResult(PutTrackResponse result) : base(result)
        {
        }

        public PutTrackCommandResult(RequestErrorType errorType, string errorMessage) : base(errorType, errorMessage)
        {
        }

        public PutTrackCommandResult(bool success, RequestErrorType errorType, string errorMessage) : base(success, errorType, errorMessage)
        {
        }
    }
}
