using System;
using System.Collections.Generic;
using TrackId.Contracts.Track;

namespace TrackId.Contracts.Genre
{
    public class GenreViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ParentGenreViewModel ParentGenre { get; set; }

        public IEnumerable<TrackViewModel> Tracks { get; set; }
    }

    public class ParentGenreViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
