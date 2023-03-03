using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MovieApp.Core.Constants;
using MovieApp.Infrastructure.Context;
using MovieApp.Web.Extensions;
using MovieApp.Web.Filters;

namespace MovieApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddServicesToDependencyInjection();
            builder.Services.AddSettingsToDependencyInjection(builder.Configuration);
            builder.Services.AddValidatorsToDependencyInjection();
            builder.Services.AddLoggingToDependencyInjection();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options => 
                {
                    options.AddSecurityDefinition(MovieAppConstants.BearerToken, new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });
                    options.OperationFilter<SecureEndpointAuthRequirementFilter>();
                });

            builder.Services.AddDbContext<MovieAppContext>(maker =>
            {
                maker.UseNpgsql(builder.Configuration.GetConnectionString(MovieAppConstants.MovieAppDatabaseSection));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            AddMiddlewareExtension.AddMiddlewareDependencyInjection(ref app);

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}