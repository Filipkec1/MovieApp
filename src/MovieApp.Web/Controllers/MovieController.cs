using Microsoft.AspNetCore.Mvc;
using MovieApp.Core.Models.Entities;
using MovieApp.Core.Services;
using MovieApp.Web.Controllers.Base;

namespace MovieApp.Web.Controllers
{
    /// <summary>
    /// Defines <see cref="Movie"/> api controller.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : BaseController<MovieController>
    {
        private IMovieService movieService;

        /// <summary>
        /// Initialize a new instance of <see cref="MovieController"/> class.
        /// </summary>
        /// <param name="service"><see cref="IMovieService"/></param>
        public MovieController(IMovieService service)
        {
            movieService = service;
        }
    }
}
