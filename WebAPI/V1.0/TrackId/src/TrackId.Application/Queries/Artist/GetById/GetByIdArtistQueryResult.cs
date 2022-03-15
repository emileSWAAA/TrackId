using TrackId.Contracts.Artist;

namespace TrackId.Application.Queries.Artist.GetById
{
    public class GetByIdArtistQueryResult : BaseQueryResponse<GetByIdArtistResponse>
    {
        public GetByIdArtistQueryResult(GetByIdArtistResponse result) : base(result)
        {
        }

        public GetByIdArtistQueryResult(RequestErrorType errorType, string errorMessage) : base(errorType, errorMessage)
        {
        }

        public GetByIdArtistQueryResult(bool success, RequestErrorType errorType, string errorMessage) : base(success, errorType, errorMessage)
        {
        }
    }
}
