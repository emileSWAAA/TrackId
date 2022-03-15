using System;
using MediatR;

namespace TrackId.Application.Commands.Artist.Delete
{
    public class DeleteArtistCommand : IRequest<DeleteArtistCommandResult>
    {
        public Guid Id { get; set; }
    }
}
