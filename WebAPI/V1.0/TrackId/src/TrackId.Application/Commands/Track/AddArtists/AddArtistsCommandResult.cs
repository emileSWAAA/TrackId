using TrackId.Application.Queries;
using TrackId.Contracts.Track.AddArtist;

namespace TrackId.Application.Commands.Track.AddArtists
{
    public class AddArtistsCommandResult : BaseQueryResponse<AddArtistsResponse>
    {
        public AddArtistsCommandResult(AddArtistsResponse result) : base(result)
        {
        }

        public AddArtistsCommandResult(RequestErrorType errorType, string errorMessage) : base(errorType, errorMessage)
        {
        }

        public AddArtistsCommandResult(bool success, RequestErrorType errorType, string errorMessage) : base(success, errorType, errorMessage)
        {
        }
    }
}
