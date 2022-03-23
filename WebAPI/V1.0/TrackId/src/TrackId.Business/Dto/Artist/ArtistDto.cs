using System.Collections.Generic;

namespace TrackId.Business.Dto
{
    public class ArtistDto : BaseDto
    {
        public string Name { get; set; }

        public string CountryCode { get; set; }

        public bool IsValidated { get; set; }

        public IList<TrackDto> Tracks { get; set; }
    }
}
