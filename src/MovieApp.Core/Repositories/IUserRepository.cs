using MovieApp.Core.Models.Entities;
using MovieApp.Core.Repositories.Base;

namespace MovieApp.Core.Repositories
{
    /// <summary>
    /// Defines <see cref="User"/> repository interface.
    /// </summary>
    public interface IUserRepository : IRepository<User, Guid>
    { }
}