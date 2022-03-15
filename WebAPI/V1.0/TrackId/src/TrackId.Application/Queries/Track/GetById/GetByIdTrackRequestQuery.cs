using System;
using MediatR;

namespace TrackId.Application.Queries.Track
{
    public class GetByIdTrackRequestQuery : IRequest<GetByIdTrackQueryResult>
    {
        public Guid Id { get; set; }
    }
}
