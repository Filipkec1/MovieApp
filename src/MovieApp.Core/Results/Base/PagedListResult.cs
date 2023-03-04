using X.PagedList;

namespace MovieApp.Core.Results.Base
{
    /// <summary>
    /// Defines paginated list result class.
    /// </summary>
    /// <typeparam name="T1">Entity form the database.</typeparam>
    /// <typeparam name="T2">A result type of <typeparamref name="T1"/>"/></typeparam>
    public class PagedListResult<T1, T2>
    {
        /// <summary>
        ///Initializes a new instance of <see cref="PagedListResult{T1, T2}"/>.
        /// </summary>
        /// <param name="pagedList"><see cref="IPagedList"/> that is being transferd into a result object.</param>
        public PagedListResult(IPagedList<T1> pagedList)
        {
            Count = pagedList.Count;
            FirstItemOnPage = pagedList.FirstItemOnPage;
            HasNextPage = pagedList.HasNextPage;
            HasPreviusPage = pagedList.HasPreviousPage;
            IsFirstPage = pagedList.IsFirstPage;
            IsLastPage = pagedList.IsLastPage;
            LastItemOnPage = pagedList.LastItemOnPage;
            PageCount = pagedList.PageCount;
            PageNumber = pagedList.PageNumber;
            PageSize = pagedList.PageSize;
        }

        public int Count { get; init; }
        public int FirstItemOnPage { get; init; }
        public bool HasNextPage { get; init; }
        public bool HasPreviusPage { get; init; }
        public bool IsFirstPage { get; init; }
        public bool IsLastPage { get; init; }
        public int LastItemOnPage { get; init; }
        public int PageCount { get; init; }
        public int PageNumber { get; init; }
        public int PageSize { get; init; }

        public IEnumerable<T2>? ResultList { get; init; }
    }
}