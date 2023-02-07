using System;

namespace TrackId.Business.Dto.Track
{
    public class TrackSourceDto : BaseDto
    {
        public string StreamSlug { get; set; }

        public bool IsBrokenLink { get; set; }

        public Guid TrackId { get; set; }

        public TrackDto Track { get; set; }

        public Guid TrackSourceTypeId { get; set; }

        public TrackSourceTypeDto TrackSourceType { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}
