using Microsoft.AspNetCore.Mvc;
using MovieApp.Core.Models.Entities;
using MovieApp.Core.Request;
using MovieApp.Core.Results.Base;
using MovieApp.Core.Results;
using MovieApp.Core.Services;
using MovieApp.Web.Controllers.Base;

namespace MovieApp.Web.Controllers
{
    /// <summary>
    /// Defines <see cref="Category"/> api controller.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : BaseController<CategoryController>
    {
        private ICategoryService categoryService;

        /// <summary>
        /// Initialize a new instance of <see cref="CategoryController"/> class.
        /// </summary>
        /// <param name="service"><see cref="ICategoryService"/></param>
        public CategoryController(ICategoryService service)
        {
            categoryService = service;
        }

        /// <summary>
        /// Get <see cref="Category"/> by Id
        /// </summary>
        /// <param name="id">Id of the searched for <see cref="Category"/></param>
        /// <returns>A <see cref="Category"/> as <see cref="CategoryResult"/></returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [Produces(typeof(CategoryResult))]
        public async Task<ActionResult<CategoryResult>> Get([FromRoute] Guid id)
        {
            Result<CategoryResult> result = await categoryService.GetCategoryById(id);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Get all <see cref="Category"/>s
        /// </summary>
        /// <returns>List of <see cref="Category"/>s as <see cref="CategoryResult"/></returns>
        [HttpGet]
        [Produces(typeof(IEnumerable<CategoryResult>))]
        public async Task<ActionResult<IEnumerable<CategoryResult>>> GetAll()
        {
            Result<IEnumerable<CategoryResult>> result = await categoryService.GetAllCategorys();

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Create new <see cref="Category"/>
        /// </summary>
        /// <param name="request"><see cref="CategoryCreateRequest"/></param>
        /// <returns>A <see cref="Category"/> as <see cref="CategoryResult"/></returns>
        [HttpPost]
        [Produces(typeof(CategoryResult))]
        public async Task<ActionResult<CategoryResult>> Post([FromBody] CategoryCreateRequest request)
        {
            Result<CategoryResult> result = await categoryService.CreateCategory(request);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value);
        }

        /// <summary>
        /// Update existing <see cref="Category"/>
        /// </summary>
        /// <param name="id">Id of the searched for <see cref="Category"/></param>
        /// <param name="request"><see cref="CategoryUpdateRequest"/></param>
        [HttpPut]
        [Route("{id:Guid}")]
        //[Authorize(RoleEnum.Admin, RoleEnum.User)]
        [Produces(typeof(NoContentResult))]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] CategoryUpdateRequest request)
        {
            Result result = await categoryService.UpdateCategory(id, request);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return NoContent();
        }
    }
}
