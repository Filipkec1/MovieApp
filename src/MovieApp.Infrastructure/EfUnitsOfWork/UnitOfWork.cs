using MovieApp.Core.Repositories;
using MovieApp.Infrastructure.Context;
using MovieApp.Infrastructure.EfRepository;

namespace MovieApp.Infrastructure.EfUnitsOfWork
{
    /// <summary>
    /// Defines unit of work.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MovieAppContext context;

        private ICategoryMovieRepository categoryMovieRepository;
        private ICategoryRepository categoryRepository;
        private IMovieRepository movieRepository;
        private IRoleRepository roleRepository;
        private IUserRepository userRepository;

        public UnitOfWork(MovieAppContext context)
        {
            this.context = context;
        }

        /// <inheritdoc/>
        public ICategoryRepository Category
        {
            get
            {
                if (categoryRepository is null)
                {
                    categoryRepository = new CategoryRepository(context);
                }

                return categoryRepository;
            }
        }

        /// <inheritdoc/>
        public ICategoryMovieRepository CategoryMovie
        {
            get
            {
                if (categoryMovieRepository is null)
                {
                    categoryMovieRepository = new CategoryMovieRepository(context);
                }

                return categoryMovieRepository;
            }
        }

        /// <inheritdoc/>
        public IMovieRepository Movie
        {
            get
            {
                if (movieRepository is null)
                {
                    movieRepository = new MovieRepository(context);
                }

                return movieRepository;
            }
        }

        /// <inheritdoc/>
        public IRoleRepository Role
        {
            get
            {
                if (roleRepository is null)
                {
                    roleRepository = new RoleRepository(context);
                }

                return roleRepository;
            }
        }

        /// <inheritdoc/>
        public IUserRepository User
        {
            get
            {
                if (userRepository is null)
                {
                    userRepository = new UserRepository(context);
                }

                return userRepository;
            }
        }

        /// <summary>
        /// Commit changes made to the databaes.
        /// </summary>
        public async Task Commit()
        {
            await context.SaveChangesAsync();
        }
    }
}