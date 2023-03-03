using Microsoft.Extensions.Logging;
using MovieApp.Core.Models.Entities;
using MovieApp.Core.Request;
using MovieApp.Core.Results.Base;
using MovieApp.Core.Results;
using MovieApp.Core.Services.Base;
using MovieApp.Infrastructure.EfUnitsOfWork;

namespace MovieApp.Core.Services
{
    /// <summary>
    /// Defines <see cref="Category"/> Service interface.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Create new <see cref="Category"/>
        /// </summary>
        /// <param name="request"><see cref="CategoryCreateRequest"/></param>
        /// <returns>A <see cref="Category"/> as <see cref="CategoryResult"/></returns>
        Task<Result<CategoryResult>> CreateCategory(CategoryCreateRequest request);

        /// <summary>
        /// Get all <see cref="Category"/>s
        /// </summary>
        /// <returns>List of <see cref="Category"/>s as <see cref="CategoryResult"/></returns>
        Task<Result<IEnumerable<CategoryResult>>> GetAllCategorys();

        /// <summary>
        /// Get <see cref="Category"/> by Id
        /// </summary>
        /// <param name="id">Id of the searched for <see cref="Category"/></param>
        /// <returns>A <see cref="Category"/> as <see cref="CategoryResult"/></returns>
        Task<Result<CategoryResult>> GetCategoryById(Guid id);

        /// <summary>
        /// Update existing <see cref="Category"/>
        /// </summary>
        /// <param name="id">Id of the searched for <see cref="Category"/></param>
        /// <param name="request"><see cref="CategoryUpdateRequest"/></param>
        /// <returns>Return success is successful, return error if an error occurred.</returns>
        Task<Result> UpdateCategory(Guid id, CategoryUpdateRequest request);
    }

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

        /// <inheritdoc/>
        public async Task<Result<CategoryResult>> CreateCategory(CategoryCreateRequest request)
        {
            //Create new category
            Category newCategory = new Category()
            {
                Name = request.Name,
            };

            //Add category to the databsae
            await unitOfWork.Category.Add(newCategory);

            //Try saving changes to the database
            try
            {
                await unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result.Failure<CategoryResult>(Error.InternalServerError);
            }

            return new CategoryResult(newCategory);
        }

        /// <inheritdoc/>
        public async Task<Result<IEnumerable<CategoryResult>>> GetAllCategorys()
        {
            //Get all movies
            IEnumerable<Category> categoryList = await unitOfWork.Category.GetAll();

            return categoryList.Select(c => new CategoryResult(c)).ToList();
        }

        /// <inheritdoc/>
        public async Task<Result<CategoryResult>> GetCategoryById(Guid id)
        {
            //Get category
            Category? category = await unitOfWork.Category.GetById(id);
            if (category == null)
            {
                return Result.Failure<CategoryResult>(new Error("404", $"Category with id {id} is missing."));
            }

            return new CategoryResult(category);
        }

        /// <inheritdoc/>
        public async Task<Result> UpdateCategory(Guid id, CategoryUpdateRequest request)
        {
            //Get category
            Category? category = await unitOfWork.Category.GetById(id);
            if (category == null)
            {
                return Result.Failure(new Error("404", $"Category with id {id} is missing."));
            }

            category.Name = request.Name;

            unitOfWork.Category.Update(category);

            //Try saving changes to the database
            try
            {
                await unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result.Failure(Error.InternalServerError);
            }

            return Result.Success();
        }
    }
}