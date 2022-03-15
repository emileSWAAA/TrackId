using System.Collections.Generic;
using TrackId.Common.Interfaces;

namespace TrackId.Business.Dto
{
    public class ArtistDto : BaseDto, IValidated
    {
        public string Name { get; set; }

        public string CountryCode { get; set; }

        public bool IsValidated { get; set; }

        public IList<TrackDto> Tracks { get; set; }
    }
}
