using MovieApp.Core.Models.Entities;

namespace MovieApp.Core.Results
{
    /// <summary>
    /// Defines <see cref="Movie"/> result class.
    /// </summary>
    public class MovieResult
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MovieResult"/>.
        /// </summary>
        /// <param name="movie"><see cref="Movie"/></param>
        public MovieResult(Movie movie)
        {
            Id = movie.Id;
            Title = movie.Title;

            //Check if movie has any categories
            if (movie.CategoryMovie.Any())
            {
                Categories = string.Join(", ", movie.CategoryMovie.Select(cm => cm.Category.Name));
            }
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Categories { get; set; } = "";
    }
}