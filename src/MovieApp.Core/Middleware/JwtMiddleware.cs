using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using MovieApp.Core.Models.Entities;
using MovieApp.Core.Results;
using MovieApp.Core.Results.Base;
using MovieApp.Core.Services;
using MovieApp.Core.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MovieApp.Core.Middleware
{
    /// <summary>
    /// Defines Jwt Middleware
    /// </summary>
    public class JwtMiddleware
    {
        private readonly RequestDelegate next;
        private readonly JWTSettings jWTSettings;

        /// <summary>
        /// Initilizes new instance of <see cref="JwtMiddleware"/>
        /// </summary>
        /// <param name="nextDelegate">Next requet.</param>
        /// <param name="settings"><see cref="JWTSettings"/></param>
        public JwtMiddleware(RequestDelegate nextDelegate, JWTSettings settings)
        {
            next = nextDelegate;
            jWTSettings = settings;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                await AttachUserToContext(context, userService, token);
            }

            await next(context);
        }

        /// <summary>
        /// Attach <see cref="User"/> to context
        /// </summary>
        /// <param name="context">The current context.</param>
        /// <param name="userService"><see cref="IUserService"/></param>
        /// <param name="token">Authorization token</param>
        private async Task AttachUserToContext(HttpContext context, IUserService userService, string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(jWTSettings.Secret);
            SecurityToken? validatedToken = null;

            try
            {
                //Validate token
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out validatedToken);
            }
            //If validation fails return nothing.
            catch
            {
                return;  
            }

            JwtSecurityToken? jwtToken = (JwtSecurityToken)validatedToken;

            //Get user Id
            if (!Guid.TryParse(jwtToken.Claims.First(x => x.Type == "Id").Value, out Guid userId))
            {
                return;
            }

            //Get User
            Result<UserResult> result = await userService.GetUserById(userId);

            //Check if result is failure
            if (result.IsFailure)
            {
                return;
            }

            //Set user to context
            context.Items["User"] = result.Value;
        }
    }
}