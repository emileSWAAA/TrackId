using System;

namespace TrackId.Contracts.Genre.GetById
{
    public class GetGenreByIdResponse : IResponseContract
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ParentGenreViewModel ParentGenre { get; set; }
    }
}
