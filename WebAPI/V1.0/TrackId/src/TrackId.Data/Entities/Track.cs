using System;
using System.Collections.Generic;
using TrackId.Common.Enum;
using TrackId.Common.Interfaces;

namespace TrackId.Data.Entities
{
    public class Track : BaseEntity, ICreateDateTime, ISoftDeletable
    {
        public string Title { get; set; }

        public TrackType Type { get; set; }

        public Guid? GenreId { get; set; }

        public Genre Genre { get; set; }

        public DateTime CreateDateTime { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeleteDateTime { get; set; }

        public virtual ICollection<ArtistTrack> Artists { get; set; }
    }
}
