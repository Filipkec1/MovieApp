using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MovieApp.Core.Models.Enums;
using MovieApp.Core.Results;

namespace MovieApp.Core.Atributes
{
    /// <summary>
    /// Defines <see cref="AuthorizeAttribute"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        //List of roles
        private readonly List<RoleEnum> roleList;

        /// <summary>
        /// Initilizes new instance of <see cref="AuthorizeAttribute"/>
        /// <param name="roles">Roles that are accpated.</param>
        public AuthorizeAttribute(params RoleEnum[] roles) : base()
        {
            roleList = roles.ToList();
        }

        /// <inheritdoc/>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //Get user from context
            UserResult? user = (UserResult?)context.HttpContext.Items["User"];

            //Check if user is null
            //If the user is null that means that they are not logged in
            if (user == null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }

            //Check if user has role
            if(!Enum.TryParse<RoleEnum>(user.Role, out RoleEnum role))
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }

            //Check if role is acceptable
            if (!roleList.Contains(role))
            {
                context.Result = new JsonResult(new { message = "User does not have permision." }) { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }
        }
    }
}