using MovieApp.Core.Models.Entities;
using MovieApp.Core.Repositories.Base;

namespace MovieApp.Core.Repositories
{
    /// <summary>
    /// Defines <see cref="CategoryMovie"/> repository interface.
    /// </summary>
    public interface ICategoryMovieRepository : IRepository<CategoryMovie, Guid>
    { }
}