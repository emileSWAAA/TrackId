using System;
using System.Collections.Generic;
using MediatR;
using TrackId.Common.Enum;

namespace TrackId.Application.Commands.Track.Post
{
    public class PostTrackCommand : IRequest<PostTrackCommandResult>
    {
        public string Title { get; set; }

        public TrackType Type { get; set; }

        public IEnumerable<Guid> Artists { get; set; }

        public Guid? GenreId { get; set; }
    }
}
