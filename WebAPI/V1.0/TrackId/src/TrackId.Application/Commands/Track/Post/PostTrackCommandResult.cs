using TrackId.Application.Queries;
using TrackId.Contracts.Track;

namespace TrackId.Application.Commands.Track.Post
{
    public class PostTrackCommandResult : BaseQueryResponse<PostTrackResponse>
    {
        public PostTrackCommandResult(PostTrackResponse result) : base(result) { }

        public PostTrackCommandResult(RequestErrorType errorType, string errorMessage) : base(errorType, errorMessage) { }

        public PostTrackCommandResult(bool success, RequestErrorType errorType, string errorMessage) : base(success, errorType, errorMessage) { }
    }
}
