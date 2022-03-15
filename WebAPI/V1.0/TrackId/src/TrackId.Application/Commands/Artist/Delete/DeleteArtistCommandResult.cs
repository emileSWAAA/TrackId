using TrackId.Application.Queries;
using TrackId.Contracts.Artist.Delete;

namespace TrackId.Application.Commands.Artist.Delete
{
    public class DeleteArtistCommandResult : BaseQueryResponse<DeleteArtistResponse>
    {
        public DeleteArtistCommandResult(DeleteArtistResponse result) : base(result)
        {
        }

        public DeleteArtistCommandResult(RequestErrorType errorType, string errorMessage) : base(errorType, errorMessage)
        {
        }

        public DeleteArtistCommandResult(bool success, RequestErrorType errorType, string errorMessage) : base(success, errorType, errorMessage)
        {
        }
    }
}
