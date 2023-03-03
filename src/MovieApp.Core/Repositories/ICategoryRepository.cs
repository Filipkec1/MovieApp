using MovieApp.Core.Models.Entities;
using MovieApp.Core.Repositories.Base;

namespace MovieApp.Core.Repositories
{
    /// <summary>
    /// Defines <see cref="Category"/> repository interface.
    /// </summary>
    public interface ICategoryRepository : IRepository<Category, Guid>
    { }
}