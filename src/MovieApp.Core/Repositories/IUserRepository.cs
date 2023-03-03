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
        /// Get <see cref="User"/> and its <see cref="Role"/> by User name.
        /// </summary>
        /// <param name="name"><see cref="User"/>'s name.</param>
        /// <returns><see cref="User"/></returns>
        Task<User?> GetUserWithRoleByName(string name);
    }
}