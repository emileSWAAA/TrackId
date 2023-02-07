using System;
using System.Collections.Generic;
using TrackId.Common.Enum;

namespace TrackId.Contracts.Track
{
    public class PostTrackResponse : IResponseContract
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<Guid> Artists { get; set; }

        public Guid? GenreId { get; set; }

        public TrackType Type { get; set; }
    }
}
