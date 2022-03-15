namespace TrackId.Contracts.Artist.Post
{
    public record PostArtistRequest : IRequestContract
    {
        public string Name { get; set; }

        public string CountryCode { get; set; }

        public bool IsValidated { get; set; }
    }
}
