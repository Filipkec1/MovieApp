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
            //services.AddHostedService<HeistAutomizeService>();
            //services.AddScoped<IHeistService, HeistService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}