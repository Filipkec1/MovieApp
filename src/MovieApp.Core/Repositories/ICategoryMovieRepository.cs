using MovieApp.Core.Models.Entities;
using MovieApp.Core.Repositories.Base;

namespace MovieApp.Core.Repositories
{
    /// <summary>
    /// Defines <see cref="CategoryMovie"/> repository interface.
    /// </summary>
    public interface ICategoryMovieRepository : IRepository<CategoryMovie, Guid>
    { 
        /// <summary>
        /// Get all <see cref="CategoryMovie"/>s that belong to a <see cref="Movie"/>.
        /// </summary>
        /// <param name="movieId">Id of the <see cref="Movie"/> that the <see cref="CategoryMovie"/>s belong to.</param>
        /// <returns>A list of <see cref="CategoryMovie"/>s</returns>
        Task<IEnumerable<CategoryMovie>> GetCategoryMoviesByMovieId(Guid movieId);
    }
}