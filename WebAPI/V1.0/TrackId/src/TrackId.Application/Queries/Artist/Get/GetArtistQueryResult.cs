using TrackId.Contracts.Artist.GetPaginated;

namespace TrackId.Application.Queries.Artist.Get
{
    public class GetArtistQueryResult : BaseQueryResponse<GetArtistPaginatedResponse>
    {
        public GetArtistQueryResult(GetArtistPaginatedResponse result) : base(result)
        {
        }

        public GetArtistQueryResult(RequestErrorType errorType, string errorMessage) : base(errorType, errorMessage)
        {
        }

        public GetArtistQueryResult(bool success, RequestErrorType errorType, string errorMessage) : base(success, errorType, errorMessage)
        {
        }
    }
}
