using System;
using System.Collections.Generic;

namespace TrackId.Contracts.Track
{
    public class PutTrackResponse : IResponseContract
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<Guid> Artists { get; set; }
    }
}
