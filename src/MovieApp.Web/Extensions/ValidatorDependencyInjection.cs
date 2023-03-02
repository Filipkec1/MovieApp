using FluentValidation.AspNetCore;

namespace MovieApp.Web.Extensions
{
    public static class ValidatorDependencyInjection
    {
        /// <summary>
        /// Adds validators to dependency injection.
        /// Add and remove unnecessary parameters if needed.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddValidatorsToDependencyInjection(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

            return services;
        }
    }
}
