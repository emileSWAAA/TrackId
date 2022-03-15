using System;
using System.Collections.Generic;

namespace TrackId.Business.Dto.Track
{
    public class CreateUpdateTrackDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<Guid> Artists { get; set; }
    }
}
