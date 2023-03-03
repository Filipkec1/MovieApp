using Microsoft.AspNetCore.Mvc;
using MovieApp.Core.Models.Entities;
using MovieApp.Core.Models.Enums;
using MovieApp.Core.Request;
using MovieApp.Core.Results;
using MovieApp.Core.Results.Base;
using MovieApp.Core.Services;
using MovieApp.Web.Controllers.Base;
using MovieApp.Core.Atributes;

namespace MovieApp.Web.Controllers
{
    /// <summary>
    /// Defines <see cref="User"/> api controller.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController<UserController>
    {
        private IUserService userService;

        /// <summary>
        /// Initialize a new instance of <see cref="UserController"/> class.
        /// </summary>
        /// <param name="service"><see cref="IUserService"/></param>
        public UserController(IUserService service)
        {
            userService = service;
        }

        /// <summary>
        /// Get all <see cref="User"/>s.
        /// </summary>
        /// <returns>List of <see cref="User"/>s as <see cref="UserResult"/></returns>
        [HttpGet]
        [Authorize(RoleEnum.Admin)]
        [Produces(typeof(IEnumerable<UserResult>))]
        public async Task<ActionResult<IEnumerable<UserResult>>> Get()
        {
            Result<IEnumerable<UserResult>> result = await userService.GetAllUsers();

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Login <see cref="User"/>
        /// </summary>
        /// <param name="request"><see cref="UserLoginRequest"/></param>
        /// <returns>A valid JWT.</returns>
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string>> Login(UserLoginRequest request)
        {
            Result<string> result = await userService.Login(request);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }
    }
}
