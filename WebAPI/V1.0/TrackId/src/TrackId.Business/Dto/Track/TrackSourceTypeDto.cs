using System;

namespace TrackId.Business.Dto.Track
{
    public class TrackSourceTypeDto : BaseDto
    {
        public string Name { get; set; }

        public string Version { get; set; }

        public string EmbeddedUrl { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}
