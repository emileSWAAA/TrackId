namespace TrackId.Data
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public JwtTokenOptions JwtTokenOptions { get; set; }
    }

    public class JwtTokenOptions
    {
        public string Secret { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }

        public string Authority { get; set; }
    }
}
