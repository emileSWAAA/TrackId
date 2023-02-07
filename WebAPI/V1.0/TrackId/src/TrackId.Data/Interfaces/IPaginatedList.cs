using System.Collections.Generic;

namespace TrackId.Data.Interfaces
{
    public interface IPaginatedList<T>
    {
        IEnumerable<T> Items { get; set; }

        int PageSize { get; }

        int PageIndex { get; }

        int TotalPages { get; }

        int TotalCount { get; }

        bool HasPreviousPage { get; }

        bool HasNextPage { get; }
    }
}