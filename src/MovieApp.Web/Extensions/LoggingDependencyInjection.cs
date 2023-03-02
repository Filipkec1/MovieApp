using Serilog;
using Serilog.Core;
using System.IO;

namespace MovieApp.Web.Extensions
{
    public static class LoggingDependencyInjection
    {
        /// <summary>
        /// Adds logging to dependency injection.
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddLoggingToDependencyInjection(this IServiceCollection services)
        {
            string path = Environment.CurrentDirectory + "/logs";
            if(!Directory.Exists(path)) 
            {
                Directory.CreateDirectory(path);
            }

            Logger logger = new LoggerConfiguration()
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .WriteTo.File(Path.Combine(path, $"{DateTime.Now:ddMMyyyy}.txt"), rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
                .CreateLogger();

            services.AddSingleton(logger);
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

            Log.Information("This is a test log message");

            return services;
        }
    }
}