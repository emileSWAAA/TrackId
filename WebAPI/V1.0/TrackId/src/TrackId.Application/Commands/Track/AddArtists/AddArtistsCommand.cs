using System;
using System.Collections.Generic;
using MediatR;

namespace TrackId.Application.Commands.Track.AddArtists
{
    public class AddArtistsCommand : IRequest<AddArtistsCommandResult>
    {
        public Guid TrackId { get; set; }

        public IEnumerable<Guid> Artists { get; set; }
    }
}
