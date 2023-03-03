using MovieApp.Core.Models.Entities;
using MovieApp.Core.Repositories.Base;
using MovieApp.Core.Request;

namespace MovieApp.Core.Repositories
{
    /// <summary>
    /// Defines <see cref="Movie"/> repository interface.
    /// </summary>
    public interface IMovieRepository : IRepository<Movie, Guid>
    { 
        /// <summary>
        /// Get <see cref="Movie"/> and all <see cref="Category"/>s that are related to the searched for Movie.
        /// </summary>
        /// <param name="id">Id of the searched for <see cref="Movie"/>.</param>
        /// <returns>A <see cref="Movie"/></returns>
        Task<Movie> GetMovieAndCategoryWithMovieId(Guid id);

        /// <summary>
        /// Get all <see cref="Movie"/>s
        /// </summary>
        /// <returns>List of <see cref="Movie"/>s</returns>
        Task<IEnumerable<Movie>> GetAllMoviesWithCategory();

        /// <summary>
        /// Get all <see cref="Movie"/> that satisfied search filter parameters
        /// </summary>
        /// <param name="request"><see cref="MovieFilterRequest"/></param>
        /// <returns>List of <see cref="Movie"/>s.</returns>
        Task<IEnumerable<Movie>> FilterMovies(MovieFilterRequest request);
    }
}