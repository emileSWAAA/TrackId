using System;
using System.Collections.Generic;
using System.Linq;
using TrackId.Data.Interfaces;

namespace TrackId.Data.Wrappers
{
    public class PaginatedList<T> : IPaginatedList<T>
    {
        public PaginatedList() { }

        public PaginatedList(int totalCount, int pageIndex, int pageSize, IEnumerable<T> items)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            if (totalCount <= pageSize)
            {
                TotalPages = 1;
            }
            else
            {
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            }
            TotalCount = totalCount;
            Items = items.ToList();
        }

        public IEnumerable<T> Items { get; set; }

        public int PageSize { get; }

        public int PageIndex { get; }

        public int TotalPages { get; }

        public int TotalCount { get; }

        //TODO: check correct PageIndex
        public bool HasPreviousPage => PageIndex > 0;

        public bool HasNextPage => PageIndex < TotalPages - 1 && TotalPages > 1;

    }
}
