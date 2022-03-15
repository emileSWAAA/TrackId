using System;
using System.Collections.Generic;
using MediatR;
using TrackId.Common.Enum;

namespace TrackId.Application.Commands.Track.Put
{
    public class PutTrackCommand : IRequest<PutTrackCommandResult>
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public TrackType Type { get; set; }

        public IEnumerable<Guid> Artists { get; set; }
    }
}
