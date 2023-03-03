using MovieApp.Core.Repositories;

namespace MovieApp.Infrastructure.EfUnitsOfWork
{
    /// <summary>
    /// Defines interface for UnitOfWork.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Used to commit changes to the underlaying persistence layer
        /// </summary>
        /// <returns></returns>
        Task Commit();

        /// <summary>
        /// Used to get <see cref="ICategoryMovieRepository"/>
        /// </summary>
        ICategoryMovieRepository CategoryMovie { get; }

        /// <summary>
        /// Used to get <see cref="ICategoryRepository"/>
        /// </summary>
        ICategoryRepository Category { get; }

        /// <summary>
        /// Used to get <see cref="IMovieRepository"/>
        /// </summary>
        IMovieRepository Movie { get; }

        /// <summary>
        /// Used to get <see cref="IRoleRepository"/>
        /// </summary>
        IRoleRepository Role { get; }

        /// <summary>
        /// Used to get <see cref="IUserRepository"/>
        /// </summary>
        IUserRepository User { get; }
    }
}