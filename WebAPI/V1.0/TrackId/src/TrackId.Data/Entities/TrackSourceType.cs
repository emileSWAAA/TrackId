using System;
using TrackId.Common.Interfaces;

namespace TrackId.Data.Entities
{
    public class TrackSourceType : BaseEntity, ICreateDateTime
    {
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public string Version { get; set; }

        public string EmbeddedUrl { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}
