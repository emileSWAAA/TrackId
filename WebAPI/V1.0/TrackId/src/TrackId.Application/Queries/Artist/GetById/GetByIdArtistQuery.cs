using System;
using MediatR;

namespace TrackId.Application.Queries.Artist.GetById
{
    public class GetByIdArtistQuery : IRequest<GetByIdArtistQueryResult>
    {
        public Guid Id { get; set; }
    }
}
