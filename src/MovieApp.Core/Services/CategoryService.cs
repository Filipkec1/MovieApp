using Microsoft.Extensions.Logging;
using MovieApp.Core.Models.Entities;
using MovieApp.Core.Services.Base;
using MovieApp.Infrastructure.EfUnitsOfWork;

namespace MovieApp.Core.Services
{
    /// <summary>
    /// Defines <see cref="Category"/> Service interface.
    /// </summary>
    public interface ICategoryService
    { }

    /// <summary>
    /// Defines <see cref="Category"/> service.
    /// </summary>
    public class CategoryService : ServiceBase<ICategoryService>, ICategoryService
    {
        /// <summary>
        /// Initilizes new instance of <see cref="CategoryService"/>
        /// </summary>
        /// <param name="unitOfWork">Unit of work.</param>
        /// <param name="logger">Logger.</param>
        public CategoryService(IUnitOfWork unitOfWork, ILogger<ICategoryService> logger) : base(unitOfWork, logger)
        { }
    }
}