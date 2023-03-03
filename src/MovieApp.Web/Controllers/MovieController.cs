using Microsoft.AspNetCore.Mvc;
using MovieApp.Core.Atributes;
using MovieApp.Core.Models.Entities;
using MovieApp.Core.Models.Enums;
using MovieApp.Core.Request;
using MovieApp.Core.Results;
using MovieApp.Core.Results.Base;
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

        /// <summary>
        /// Delete <see cref="Movie"/>
        /// </summary>
        /// <param name="id">Id of the <see cref="Movie"/> that is going to be deleted</param>
        [HttpDelete]
        [Route("{id:Guid}")]
        //[Authorize(RoleEnum.Admin)]
        [Produces(typeof(NoContentResult))]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Result result = await movieService.DeleteMovie(id);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return NoContent();
        }

        /// <summary>
        /// Get <see cref="Movie"/> by Id
        /// </summary>
        /// <param name="id">Id of the searched for <see cref="Movie"/></param>
        /// <returns>A <see cref="Movie"/> as <see cref="MovieResult"/></returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [Produces(typeof(MovieResult))]
        public async Task<ActionResult<MovieResult>> Get([FromRoute] Guid id)
        {
            Result<MovieResult> result = await movieService.GetMovieById(id);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Get all <see cref="Movie"/>s
        /// </summary>
        /// <returns>List of <see cref="Movie"/>s as <see cref="MovieResult"/></returns>
        [HttpGet]
        [Produces(typeof(IEnumerable<MovieResult>))]
        public async Task<ActionResult<IEnumerable<MovieResult>>> GetAll()
        {
            Result<IEnumerable<MovieResult>> result = await movieService.GetAllMovies();

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Get all <see cref="Movie"/> that satisfied search filter parameters
        /// </summary>
        /// <param name="request"><see cref="MovieFilterRequest"/></param>
        /// <returns>List of <see cref="Movie"/>s as <see cref="MovieResult"/></returns>
        [HttpGet]
        [Route("filter")]
        [Produces(typeof(IEnumerable<MovieResult>))]
        public async Task<ActionResult<IEnumerable<MovieResult>>> FilterMovie([FromQuery] MovieFilterRequest request)
        {
            Result<IEnumerable<MovieResult>> result = await movieService.FilterMovies(request);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Create new <see cref="Movie"/>
        /// </summary>
        /// <param name="request"><see cref="MovieCreateRequest"/></param>
        /// <returns>A <see cref="Movie"/> as <see cref="MovieResult"/></returns>
        [HttpPost]
        [Produces(typeof(MovieResult))]
        public async Task<ActionResult<MovieResult>> Post([FromBody] MovieCreateRequest request)
        {
            Result<MovieResult> result = await movieService.CreateMovie(request);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value);
        }

        /// <summary>
        /// Update existing <see cref="Movie"/>
        /// </summary>
        /// <param name="id">Id of the searched for <see cref="Movie"/></param>
        /// <param name="request"><see cref="MovieUpdateRequest"/></param>
        [HttpPut]
        [Route("{id:Guid}")]
        //[Authorize(RoleEnum.Admin, RoleEnum.User)]
        [Produces(typeof(NoContentResult))]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] MovieUpdateRequest request)
        {
            Result result = await movieService.UpdateMovie(id, request);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return NoContent();
        }
    }
}