using System;
using System.Collections.Generic;
using TrackId.Common.Interfaces;

namespace TrackId.Data.Entities
{
    public class Artist : BaseEntity, IValidated, ICreateDateTime, ISoftDeletable
    {
        public string Name { get; set; }

        public DateTime CreateDateTime { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeleteDateTime { get; set; }

        public virtual ICollection<ArtistTrack> Tracks { get; set; }

        public bool IsValidated { get; set; }
    }
}
