using Microsoft.AspNetCore.Mvc;
using MovieApp.Core.Models.Entities;
using MovieApp.Core.Results;
using MovieApp.Core.Results.Base;
using MovieApp.Core.Services;
using MovieApp.Web.Controllers.Base;

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
    }
}
