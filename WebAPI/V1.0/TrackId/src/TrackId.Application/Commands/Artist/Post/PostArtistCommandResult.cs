using TrackId.Application.Queries;
using TrackId.Contracts.Artist.Post;

namespace TrackId.Application.Commands.Artist.Post
{
    public class PostArtistCommandResult : BaseQueryResponse<PostArtistResponse>
    {
        public PostArtistCommandResult(PostArtistResponse result) : base(result)
        {
        }

        public PostArtistCommandResult(RequestErrorType errorType, string errorMessage) : base(errorType, errorMessage)
        {
        }

        public PostArtistCommandResult(bool success, RequestErrorType errorType, string errorMessage) : base(success, errorType, errorMessage)
        {
        }
    }
}
