using MovieApp.Core.Models.Entities;
using MovieApp.Core.Repositories.Base;

namespace MovieApp.Core.Repositories
{
    /// <summary>
    /// Defines <see cref="Movie"/> repository interface.
    /// </summary>
    public interface IMovieRepository : IRepository<Movie, Guid>
    { }
}