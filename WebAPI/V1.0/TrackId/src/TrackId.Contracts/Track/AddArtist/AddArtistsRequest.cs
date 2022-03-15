using System;
using System.Collections.Generic;

namespace TrackId.Contracts.Track.AddArtist
{
    public class AddArtistsRequest : IRequestContract
    {
        public IEnumerable<Guid> Artists { get; set; }
    }
}
