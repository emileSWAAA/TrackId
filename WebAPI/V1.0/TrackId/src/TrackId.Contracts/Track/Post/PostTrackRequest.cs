using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TrackId.Common.Enum;

namespace TrackId.Contracts.Track
{
    public class PostTrackRequest : IRequestContract
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public IEnumerable<Guid> Artists { get; set; }

        public Guid? GenreId { get; set; }

        public TrackType Type { get; set; }
    }
}
