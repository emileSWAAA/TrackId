using System;
using System.Collections.Generic;
using TrackId.Contracts.Artist;

namespace TrackId.Contracts.Track
{
    public class GetByIdTrackResponse : IResponseContract
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<ArtistViewModel> Artists { get; set; }
    }
}
