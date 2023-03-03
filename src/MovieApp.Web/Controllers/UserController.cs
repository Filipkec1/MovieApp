using Microsoft.AspNetCore.Mvc;
using MovieApp.Core.Models.Entities;
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
        /// <summary>
        /// Initialize a new instance of <see cref="UserController"/> class.
        /// </summary>
        /// <param name="service"><see cref="IHeistService"/></param>
        public UserController(IHeistService service)
        {
            heistService = service;
        }
    }
}
