using MovieApp.Core.Models.Entities;
using MovieApp.Core.Results.Base;
using X.PagedList;

namespace MovieApp.Core.Results
{
    /// <summary>
    /// Defines <see cref="MoviePagedResult"/> result class.
    /// </summary>
    public class MoviePagedResult : PagedListResult<Movie, MovieResult>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MoviePagedResult"/>.
        /// </summary>
        /// <param name="pagedList">Paged list of <see cref="Movie"/>s that needs to be transformed into a paged list of <see cref="MovieResult"/>s.</param>
        public MoviePagedResult(IPagedList<Movie> pagedList) : base(pagedList)
        {
            ResultList = pagedList.Select(m => new MovieResult(m));
        }
    }
}