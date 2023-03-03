using MovieApp.Core.Models.Entities;

namespace MovieApp.Core.Request
{
    /// <summary>
    /// Defines <see cref="Movie"/> create request.
    /// </summary>
    public class MovieCreateRequest
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MovieCreateRequest"/>.
        /// </summary>
        public MovieCreateRequest()
        {
            CategoryIdList = new List<Guid>();
        }

        public string Title { get; set; }
        public List<Guid> CategoryIdList { get; set; }
    }
}