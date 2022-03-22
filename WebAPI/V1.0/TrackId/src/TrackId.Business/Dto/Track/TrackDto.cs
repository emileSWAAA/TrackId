using System;
using System.Collections.Generic;
using TrackId.Common.Enum;
using TrackId.Data.Entities;

namespace TrackId.Business.Dto
{
    public class TrackDto : BaseDto
    {
        public string Title { get; set; }

        public TrackType Type { get; set; }

        public IEnumerable<ArtistDto> Artists { get; set; }

        public Guid? GenreId { get; set; }
    }
}
