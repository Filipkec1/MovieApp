using MovieApp.Core.Models.Entities;

namespace MovieApp.Core.Request
{
    /// <summary>
    /// Defines <see cref="Movie"/> update request.
    /// </summary>
    public class MovieFilterRequest
    {
        public string Name { get; set; }
    }
}