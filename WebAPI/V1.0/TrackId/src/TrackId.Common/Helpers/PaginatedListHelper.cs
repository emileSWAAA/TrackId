using TrackId.Common.Constants;

namespace TrackId.Common.Helpers
{
    public static class PaginatedListHelper
    {
        public static int ParsePageSize(int pageSize)
        {
            if (pageSize < 0)
            {
                return 0;
            }

            if (pageSize > ApplicationConstants.MaxPageSize)
            {
                return ApplicationConstants.MaxPageSize;
            }

            return pageSize;
        }

        public static int ParsePageIndex(int pageIndex)
        {
            if (pageIndex < 0)
            {
                return 0;
            }

            return pageIndex;
        }

        public static int ParseSkip(int pageIndex, int pageSize)
        {
            pageIndex = ParsePageIndex(pageIndex);
            pageSize = ParsePageSize(pageSize);

            var skip = (pageIndex) * pageSize;
            if (skip < 0)
            {
                return 0;
            }

            return skip;
        }
    }
}
