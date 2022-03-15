using System;
using MediatR;

namespace TrackId.Application.Commands.Track.Delete
{
    public class DeleteTrackCommand : IRequest<DeleteTrackCommandResult>
    {
        public Guid Id { get; set; }
    }
}
