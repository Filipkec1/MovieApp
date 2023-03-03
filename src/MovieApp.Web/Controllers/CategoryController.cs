using Microsoft.AspNetCore.Mvc;
using MovieApp.Core.Models.Entities;
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
    }
}
