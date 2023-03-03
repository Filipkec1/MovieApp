using MovieApp.Core.Helpers;
using MovieApp.Core.Services;
using MovieApp.Infrastructure.EfUnitsOfWork;

namespace MovieApp.Web.Extensions
{
    public static class ServiceDependencyInjection
    {
        /// <summary>
        /// Adds services to dependency injection.
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddServicesToDependencyInjection(this IServiceCollection services)
        {
            //Category
            services.AddScoped<ICategoryService, CategoryService>();

            //Movie
            services.AddScoped<IMovieService, MovieService>();

            //User
            services.AddScoped<IUserService, UserService>();

            //Password Hashing
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            //Unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}