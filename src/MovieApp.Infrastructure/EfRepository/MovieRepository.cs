using Microsoft.EntityFrameworkCore;
using MovieApp.Core.Models.Entities;
using MovieApp.Core.Repositories;
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
        public override async Task<Movie?> GetById(Guid id)
        {
            return await GetTableQueryable()
                        .AsNoTracking()
                        .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}