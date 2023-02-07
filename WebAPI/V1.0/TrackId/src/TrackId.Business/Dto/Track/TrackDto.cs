using System;
using System.Collections.Generic;
using TrackId.Business.Dto.Track;
using TrackId.Common.Enum;
using TrackId.Data.Entities;

namespace TrackId.Business.Dto
{
    public class TrackDto : BaseDto
    {
        public string Title { get; set; }

        public TrackType Type { get; set; }

        public IEnumerable<ArtistDto> Artists { get; set; }

        public IEnumerable<TrackSourceDto> TrackSources { get; set; }

        public Guid? GenreId { get; set; }
    }
}
