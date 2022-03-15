using TrackId.Application.Queries;
using TrackId.Contracts.Artist.Put;

namespace TrackId.Application.Commands.Artist.Put
{
    public class PutArtistCommandResult : BaseQueryResponse<PutArtistResponse>
    {
        public PutArtistCommandResult(PutArtistResponse result) : base(result)
        {
        }

        public PutArtistCommandResult(RequestErrorType errorType, string errorMessage) : base(errorType, errorMessage)
        {
        }

        public PutArtistCommandResult(bool success, RequestErrorType errorType, string errorMessage) : base(success, errorType, errorMessage)
        {
        }
    }
}
