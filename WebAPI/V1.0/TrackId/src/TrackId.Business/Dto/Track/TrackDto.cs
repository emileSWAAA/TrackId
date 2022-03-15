using System;
using System.Collections.Generic;
using TrackId.Common.Enum;

namespace TrackId.Business.Dto
{
    public class TrackDto : BaseDto
    {
        public string Title { get; set; }

        public TrackType Type { get; set; }

        public IEnumerable<Guid> Artists { get; set; } = new List<Guid>();
    }
}
