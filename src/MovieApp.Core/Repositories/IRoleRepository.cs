using MovieApp.Core.Models.Entities;
using MovieApp.Core.Repositories.Base;

namespace MovieApp.Core.Repositories
{
    /// <summary>
    /// Defines <see cref="Role"/> repository interface.
    /// </summary>
    public interface IRoleRepository : IRepository<Role, Guid>
    { }
}