using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TrackId.Common.Enum;

namespace TrackId.Contracts.Track
{
    public class PutTrackRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public IEnumerable<Guid> Artists { get; set; }

        public TrackType TrackType { get; set; }
    }
}
