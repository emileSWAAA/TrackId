using MediatR;

namespace TrackId.Application.Queries.Artist.Get
{
    public class GetArtistRequestQuery : IRequest<GetArtistQueryResult>
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
