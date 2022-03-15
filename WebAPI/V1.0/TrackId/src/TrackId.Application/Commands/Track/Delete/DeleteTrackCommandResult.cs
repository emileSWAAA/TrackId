using TrackId.Application.Queries;
using TrackId.Contracts.Track.Delete;

namespace TrackId.Application.Commands.Track.Delete
{
    public class DeleteTrackCommandResult : BaseQueryResponse<DeleteTrackResponse>
    {
        public DeleteTrackCommandResult(DeleteTrackResponse result) : base(result)
        {
        }

        public DeleteTrackCommandResult(RequestErrorType errorType, string errorMessage) : base(errorType, errorMessage)
        {
        }

        public DeleteTrackCommandResult(bool success, RequestErrorType errorType, string errorMessage) : base(success, errorType, errorMessage)
        {
        }
    }
}
