using TrackId.Data.Wrappers;

namespace TrackId.Contracts.Genre.GetPaginated
{
    public class GetGenrePaginatedResponse : IResponseContract
    {
        public PaginatedList<GenreViewModel> Result { get; set; }
    }
}
