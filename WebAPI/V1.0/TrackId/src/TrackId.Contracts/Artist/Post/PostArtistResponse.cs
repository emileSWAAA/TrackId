using System;

namespace TrackId.Contracts.Artist.Post
{
    public record PostArtistResponse : IResponseContract
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string CountryCode { get; set; }
    }
}
