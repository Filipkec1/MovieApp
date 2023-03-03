using Microsoft.EntityFrameworkCore;
using MovieApp.Core.Models.Entities;
using MovieApp.Core.Repositories;
using MovieApp.Infrastructure.Context;
using MovieApp.Infrastructure.EfRepository.Base;

namespace MovieApp.Infrastructure.EfRepository
{
    /// <summary>
    /// Defines repository for <see cref="Category"/>.
    /// </summary>
    public class CategoryRepository : RepositoryBase<Category, Guid>, ICategoryRepository
    {
        /// <summary>
        /// Initialize new instance of the <see cref="CategoryRepository"/> class.
        /// </summary>
        /// <param name="context"><see cref="MovieAppContext"/></param>
        public CategoryRepository(MovieAppContext context) : base(context)
        { }

        /// <inheritdoc />
        public override async Task<Category?> GetById(Guid id)
        {
            return await GetTableQueryable()
                        .AsNoTracking()
                        .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}