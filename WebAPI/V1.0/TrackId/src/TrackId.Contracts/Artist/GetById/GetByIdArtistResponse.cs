using System;
using System.Collections.Generic;
using TrackId.Contracts.Track;

namespace TrackId.Contracts.Artist
{
    public record GetByIdArtistResponse : IResponseContract
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<TrackViewModel> Tracks { get; set; }

    }
}
