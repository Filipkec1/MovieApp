using MovieApp.Core.Models.Entities;
using MovieApp.Core.Request.Base;

namespace MovieApp.Core.Request
{
    /// <summary>
    /// Defines <see cref="Movie"/> update request.
    /// </summary>
    public class MovieFilterRequest : PaginatedListRequest
    {
        public string Title { get; set; }
    }
}