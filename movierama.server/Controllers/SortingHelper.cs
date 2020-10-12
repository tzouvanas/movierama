using System;

namespace Movierama.Server.Views.Home
{
    public enum SortBy
    {
        None,
        Likes,
        Hates,
        Date,
        PublicationDate
    }

    public enum SortOrder
    {
        Asc,
        Desc
    }

    public static class SortingHelper
    {
        public static SortOrder SwapSortOrder(SortOrder sortOrder)
        {
            switch (sortOrder) {
                case SortOrder.Asc:
                    return SortOrder.Desc;
                case SortOrder.Desc:
                    return SortOrder.Asc;
            }

            return SortOrder.Asc;
        }

        public static SortBy ResolveSortBy(string sortBy) 
        {
            if (string.IsNullOrEmpty(sortBy))
                return SortBy.None;

            var sortByValue = Enum.Parse<SortBy>(sortBy);

            return sortByValue;
        }

        public static SortOrder ResolveSortOrder(string sortOrder)
        {
            if (string.IsNullOrEmpty(sortOrder))
                return SortOrder.Asc;

            var sortOrderValue = Enum.Parse<SortOrder>(sortOrder);

            return sortOrderValue;
        }

        internal static void UpdateViewBag(dynamic ViewBag, SortBy sortByValue, SortOrder sortOrderValue)
        {
            ViewBag.SortType = sortByValue;

            if (sortByValue == SortBy.Date)
                ViewBag.DateSortOrder = SortingHelper.SwapSortOrder(sortOrderValue);

            if (sortByValue == SortBy.PublicationDate)
                ViewBag.PublicationDateSortOrder = SortingHelper.SwapSortOrder(sortOrderValue);

            if (sortByValue == SortBy.Likes)
                ViewBag.LikesSortOrder = SortingHelper.SwapSortOrder(sortOrderValue);

            if (sortByValue == SortBy.Hates)
                ViewBag.HatesSortOrder = SortingHelper.SwapSortOrder(sortOrderValue);
        }
    }
}
