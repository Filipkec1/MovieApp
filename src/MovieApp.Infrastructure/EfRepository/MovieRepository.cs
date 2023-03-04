using LinqKit;
using Microsoft.EntityFrameworkCore;
using MovieApp.Core.Models.Entities;
using MovieApp.Core.Repositories;
using MovieApp.Core.Request;
using MovieApp.Core.Request.Base;
using MovieApp.Infrastructure.Context;
using MovieApp.Infrastructure.EfRepository.Base;
using X.PagedList;

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
        public async Task<IPagedList<Movie>> FilterMovies(MovieFilterRequest request)
        {
            ExpressionStarter<Movie> predicate = PredicateBuilder.New<Movie>();

            //Title
            if (!string.IsNullOrEmpty(request.Title))
            {
                predicate = predicate.And(p => p.Title.ToLower().Contains(request.Title.ToLower()));
            }

            return await GetTableQueryable()
                        .AsNoTracking()
                        .Include(m => m.CategoryMovie)
                        .ThenInclude(cm => cm.Category)
                        .Where(predicate)
                        .ToPagedListAsync(request.Page, request.Size);
        }

        /// <inheritdoc />
        public async Task<IPagedList<Movie>> GetAllMoviesWithCategory(PaginatedListRequest request)
        {
            return await GetTableQueryable()
                        .AsNoTracking()
                        .Include(m => m.CategoryMovie)
                        .ThenInclude(cm => cm.Category)
                        .ToPagedListAsync(request.Page, request.Size);
        }

        /// <inheritdoc />
        public override async Task<Movie?> GetById(Guid id)
        {
            return await GetTableQueryable()
                        .AsNoTracking()
                        .FirstOrDefaultAsync(u => u.Id == id);
        }

        /// <inheritdoc />
        public async Task<Movie?> GetMovieAndCategoryWithMovieId(Guid id)
        {
            return await GetTableQueryable()
                        .AsNoTracking()
                        .Include(m => m.CategoryMovie)
                        .ThenInclude(cm => cm.Category)
                        .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}