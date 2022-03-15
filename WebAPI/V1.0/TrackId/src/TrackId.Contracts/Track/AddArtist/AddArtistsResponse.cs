using System;

namespace TrackId.Contracts.Track.AddArtist
{
    public class AddArtistsResponse : IResponseContract
    {
        public Guid TrackId { get; set; }
    }
}
