using MovieApp.Core.Models.Entities;
using MovieApp.Core.Repositories.Base;

namespace MovieApp.Core.Repositories
{
    /// <summary>
    /// Defines <see cref="User"/> repository interface.
    /// </summary>
    public interface IUserRepository : IRepository<User, Guid>
    {
        /// <summary>
        /// Get all <see cref="User"/>s with their <see cref="Role"/>
        /// </summary>
        /// <returns>A list of <see cref="User"/>s.</returns>
        Task<IEnumerable<User>> GetAllUsers();
    }
}