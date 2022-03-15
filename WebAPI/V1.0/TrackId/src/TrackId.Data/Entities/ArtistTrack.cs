using System;
using System.ComponentModel.DataAnnotations;
using TrackId.Common.Interfaces;

namespace TrackId.Data.Entities
{
    public class ArtistTrack : BaseEntity, ICreateDateTime, ISoftDeletable
    {
        [Required]
        public Guid ArtistId { get; set; }

        [Required]
        public virtual Artist Artist { get; set; }

        [Required]
        public Guid TrackId { get; set; }

        public virtual Track Track { get; set; }



        public DateTime CreateDateTime { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeleteDateTime { get; set; }
    }
}
