using System;
using TrackId.Common.Interfaces;

namespace TrackId.Data.Entities
{
    public class TrackSource : BaseEntity, ICreateDateTime
    {
        public string StreamSlug { get; set; }

        public bool IsBrokenLink { get; set; }

        public Guid TrackId { get; set; }

        public Track Track { get; set; }

        public Guid TrackSourceTypeId { get; set; }

        public TrackSourceType TrackSourceType { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}
