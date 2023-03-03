using Microsoft.EntityFrameworkCore;
using MovieApp.Core.Models.Entities;
using MovieApp.Core.Repositories;
using MovieApp.Infrastructure.Context;
using MovieApp.Infrastructure.EfRepository.Base;

namespace MovieApp.Infrastructure.EfRepository
{
    /// <summary>
    /// Defines repository for <see cref="CategoryMovie"/>.
    /// </summary>
    public class CategoryMovieRepository : RepositoryBase<CategoryMovie, Guid>, ICategoryMovieRepository
    {
        /// <summary>
        /// Initialize new instance of the <see cref="CategoryMovieRepository"/> class.
        /// </summary>
        /// <param name="context"><see cref="MovieAppContext"/></param>
        public CategoryMovieRepository(MovieAppContext context) : base(context)
        { }

        /// <inheritdoc />
        public override async Task<CategoryMovie?> GetById(Guid id)
        {
            return await GetTableQueryable()
                        .AsNoTracking()
                        .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}