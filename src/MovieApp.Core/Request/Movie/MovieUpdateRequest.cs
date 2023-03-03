namespace MovieApp.Core.Request
{
    /// <summary>
    /// Defines <see cref="Movie"/> update request.
    /// </summary>
    public class MovieUpdateRequest
    {

        /// <summary>
        /// Initializes a new instance of <see cref="MovieUpdateRequest"/>.
        /// </summary>
        public MovieUpdateRequest()
        {
            CategoryIdList = new List<Guid>();
        }

        public string Title { get; set; }
        public List<Guid> CategoryIdList { get; set; }
    }
}