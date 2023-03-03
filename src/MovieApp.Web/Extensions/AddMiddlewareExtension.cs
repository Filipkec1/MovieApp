using MovieApp.Core.Middleware;

namespace MovieApp.Web.Extensions
{
    /// <summary>
    /// Adds middleware to dependency injection.
    /// </summary>
    public static class AddMiddlewareExtension
    {
        /// <summary>
        /// Adds middleware to dependency injection.
        /// </summary>
        /// <param name="services"></param>
        public static void AddMiddlewareDependencyInjection(ref WebApplication app)
        {
            app.UseMiddleware<JwtMiddleware>();
        }
    }
}
