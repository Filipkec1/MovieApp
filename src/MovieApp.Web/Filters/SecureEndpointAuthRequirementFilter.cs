using Microsoft.OpenApi.Models;
using MovieApp.Core.Atributes;
using MovieApp.Core.Constants;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MovieApp.Web.Filters
{
    public class SecureEndpointAuthRequirementFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (!context.ApiDescription
                .ActionDescriptor
                .EndpointMetadata
                .OfType<AuthorizeAttribute>()
                .Any())
            {
                return;
            }

            operation.Security = new List<OpenApiSecurityRequirement>
        {
            new OpenApiSecurityRequirement
            {
                [new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = MovieAppConstants.BearerToken }
                }] = new List<string>()
            }
        };
        }
    }
}
