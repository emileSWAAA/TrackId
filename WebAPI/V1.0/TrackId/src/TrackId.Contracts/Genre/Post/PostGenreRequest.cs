using System;
using System.ComponentModel.DataAnnotations;

namespace TrackId.Contracts.Genre.Post
{
    public class PostGenreRequest : IRequestContract
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public Guid? ParentGenre { get; set; }
    }
}
