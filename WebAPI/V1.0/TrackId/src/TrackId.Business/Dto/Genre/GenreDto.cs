using System.Collections.Generic;
using TrackId.Data.Interfaces;

namespace TrackId.Business.Dto.Genre
{
    public class GenreDto : BaseDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public GenreDto ParentGenre { get; set; }

        public IPaginatedList<TrackDto> Tracks { get; set; }
    }
}
