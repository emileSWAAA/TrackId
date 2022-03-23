using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TrackId.Common.Interfaces;

namespace TrackId.Data.Entities
{
    public class Genre : BaseEntity, ICreateDateTime, ISoftDeletable
    {
        [Required]
        [StringLength(75)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        public Guid? ParentGenreId { get; set; }

        public Genre ParentGenre { get; set; }

        public DateTime CreateDateTime { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeleteDateTime { get; set; }

        public ICollection<Track> Tracks { get; set; }
    }
}
