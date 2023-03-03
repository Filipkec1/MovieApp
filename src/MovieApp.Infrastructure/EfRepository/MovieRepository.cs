using Microsoft.EntityFrameworkCore;
using MovieApp.Core.Models.Entities;
using MovieApp.Core.Repositories;
using MovieApp.Core.Request;
using MovieApp.Infrastructure.Context;
using MovieApp.Infrastructure.EfRepository.Base;

namespace MovieApp.Infrastructure.EfRepository
{
    /// <summary>
    /// Defines repository for <see cref="Movie"/>.
    /// </summary>
    public class MovieRepository : RepositoryBase<Movie, Guid>, IMovieRepository
    {
        /// <summary>
        /// Initialize new instance of the <see cref="MovieRepository"/> class.
        /// </summary>
        /// <param name="context"><see cref="MovieAppContext"/></param>
        public MovieRepository(MovieAppContext context) : base(context)
        { }

        /// <inheritdoc />
        public Task<IEnumerable<Movie>> FilterMovies(MovieFilterRequest request)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Movie>> GetAllMoviesWithCategory()
        {
            return await GetTableQueryable()
                        .AsNoTracking()
                        .Include(m => m.CategoryMovie)
                        .ThenInclude(cm => cm.Category)
                        .ToListAsync();
        }

        /// <inheritdoc />
        public override async Task<Movie?> GetById(Guid id)
        {
            return await GetTableQueryable()
                        .AsNoTracking()
                        .FirstOrDefaultAsync(u => u.Id == id);
        }

        /// <inheritdoc />
        public async Task<Movie> GetMovieAndCategoryWithMovieId(Guid id)
        {
            return await GetTableQueryable()
                        .AsNoTracking()
                        .Include(m => m.CategoryMovie)
                        .ThenInclude(cm => cm.Category)
                        .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}