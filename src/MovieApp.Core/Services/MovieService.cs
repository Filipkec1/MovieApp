using Microsoft.Extensions.Logging;
using MovieApp.Core.Models.Entities;
using MovieApp.Core.Request;
using MovieApp.Core.Results;
using MovieApp.Core.Results.Base;
using MovieApp.Core.Services.Base;
using MovieApp.Infrastructure.EfUnitsOfWork;

namespace MovieApp.Core.Services
{
    /// <summary>
    /// Defines <see cref="Movie"/> Service interface.
    /// </summary>
    public interface IMovieService
    {
        /// <summary>
        /// Create new <see cref="Movie"/>
        /// </summary>
        /// <param name="request"><see cref="MovieCreateRequest"/></param>
        /// <returns>A <see cref="Movie"/> as <see cref="MovieResult"/></returns>
        Task<Result<MovieResult>> CreateMovie(MovieCreateRequest request);

        /// <summary>
        /// Delete <see cref="Movie"/>
        /// </summary>
        /// <param name="id">Id of the <see cref="Movie"/> that is going to be deleted.</param>
        /// <returns>Return success is successful, return error if an error occurred.</returns>
        Task<Result> DeleteMovie(Guid id);

        /// <summary>
        /// Get all <see cref="Movie"/> that satisfied search filter parameters
        /// </summary>
        /// <param name="request"><see cref="MovieFilterRequest"/></param>
        /// <returns>List of <see cref="Movie"/>s as <see cref="MovieResult"/></returns>
        Task<Result<IEnumerable<MovieResult>>> FilterMovies(MovieFilterRequest request);

        /// <summary>
        /// Get all <see cref="Movie"/>s
        /// </summary>
        /// <returns>List of <see cref="Movie"/>s as <see cref="MovieResult"/></returns>
        Task<Result<IEnumerable<MovieResult>>> GetAllMovies();

        /// <summary>
        /// Get <see cref="Movie"/> by Id
        /// </summary>
        /// <param name="id">Id of the searched for <see cref="Movie"/></param>
        /// <returns>A <see cref="Movie"/> as <see cref="MovieResult"/></returns>
        Task<Result<MovieResult>> GetMovieById(Guid id);

        /// <summary>
        /// Update existing <see cref="Movie"/>
        /// </summary>
        /// <param name="id">Id of the searched for <see cref="Movie"/></param>
        /// <param name="request"><see cref="MovieUpdateRequest"/></param>
        /// <returns>Return success is successful, return error if an error occurred.</returns>
        Task<Result> UpdateMovie(Guid id, MovieUpdateRequest request);
    }

    /// <summary>
    /// Defines <see cref="Movie"/> service.
    /// </summary>
    public class MovieService : ServiceBase<IMovieService>, IMovieService
    {
        /// <summary>
        /// Initilizes new instance of <see cref="MovieService"/>
        /// </summary>
        /// <param name="unitOfWork">Unit of work.</param>
        /// <param name="logger">Logger.</param>
        public MovieService(IUnitOfWork unitOfWork, ILogger<IMovieService> logger) : base(unitOfWork, logger)
        { }

        /// <inheritdoc/>
        public async Task<Result<MovieResult>> CreateMovie(MovieCreateRequest request)
        {
            //Create new movie
            Movie newMovie = new Movie()
            {
                Title = request.Title
            };

            //Add movie to the databsae
            await unitOfWork.Movie.Add(newMovie);

            //Check if a Category id is repeted
            HashSet<Guid> idHash = new HashSet<Guid>(request.CategoryIdList);
            if (idHash.Count != request.CategoryIdList.Count)
            {
                return Result.Failure<MovieResult>(new Error("400", "A category is repeted."));
            }

            //CategoryMovie list that is going be created
            List<CategoryMovie> categoryMovieCreateList = FillCreateCategoryMovieList(newMovie.Id, request.CategoryIdList);

            //Check if categoryMovieCreateList has any values
            //If it has add them to the database
            if (categoryMovieCreateList.Any())
            {
                await unitOfWork.CategoryMovie.AddRange(categoryMovieCreateList);
            }

            //Try saving changes to the database
            try
            {
                await unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result.Failure<MovieResult>(Error.InternalServerError);
            }

            //Get movie
            Movie? movie = await unitOfWork.Movie.GetMovieAndCategoryWithMovieId(newMovie.Id);
            return new MovieResult(movie);
        }

        /// <inheritdoc/>
        public async Task<Result> DeleteMovie(Guid id)
        {
            //Get movie
            Movie movie = await unitOfWork.Movie.GetById(id);
            if (movie == null)
            {
                return Result.Failure<MovieResult>(new Error("404", $"Movie with id {id} is missing."));
            }

            //CategoryMovie list that is going be created
            IEnumerable<CategoryMovie> categoryMovieDeleteList = await unitOfWork.CategoryMovie.GetCategoryMoviesByMovieId(id);

            unitOfWork.Movie.Delete(movie);

            //Check if categoryMovieDeleteList has any values
            //If it has delete them from the database
            if (categoryMovieDeleteList.Any())
            {
                unitOfWork.CategoryMovie.DeleteRange(categoryMovieDeleteList);
            }

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

        /// <inheritdoc/>
        public async Task<Result<IEnumerable<MovieResult>>> FilterMovies(MovieFilterRequest request)
        {
            //Get filtered movies
            IEnumerable<Movie> movieList = await unitOfWork.Movie.FilterMovies(request);

            return movieList.Select(m => new MovieResult(m)).ToList();
        }

        /// <inheritdoc/>
        public async Task<Result<IEnumerable<MovieResult>>> GetAllMovies()
        {
            //Get all movies
            IEnumerable<Movie> movieList = await unitOfWork.Movie.GetAllMoviesWithCategory();

            return movieList.Select(m => new MovieResult(m)).ToList();
        }

        /// <inheritdoc/>
        public async Task<Result<MovieResult>> GetMovieById(Guid id)
        {
            //Get movie
            Movie movie = await unitOfWork.Movie.GetMovieAndCategoryWithMovieId(id);
            if (movie == null)
            {
                return Result.Failure<MovieResult>(new Error("404", $"Movie with id {id} is missing."));
            }

            return new MovieResult(movie);
        }

        /// <inheritdoc/>
        public async Task<Result> UpdateMovie(Guid id, MovieUpdateRequest request)
        {
            //Get movie
            Movie movie = await unitOfWork.Movie.GetById(id);
            if (movie == null)
            {
                return Result.Failure<MovieResult>(new Error("404", $"Movie with id {id} is missing."));
            }

            //Update movie
            movie.Title = request.Title;

            //Check if a Category id is repeted
            HashSet<Guid> idHash = new HashSet<Guid>(request.CategoryIdList);
            if (idHash.Count != request.CategoryIdList.Count)
            {
                return Result.Failure<MovieResult>(new Error("400", "A category is repeted."));
            }

            //CategoryMovie list that is going be created
            List<CategoryMovie> categoryMovieCreateList = FillCreateCategoryMovieList(id, request.CategoryIdList);

            //CategoryMovie list that is going be created
            IEnumerable<CategoryMovie> categoryMovieDeleteList = await unitOfWork.CategoryMovie.GetCategoryMoviesByMovieId(id);

            unitOfWork.Movie.Update(movie);

            //Check if categoryMovieCreateList has any values
            //If it has add them to the database
            if (categoryMovieCreateList.Any())
            {
                await unitOfWork.CategoryMovie.AddRange(categoryMovieCreateList);
            }

            //Check if categoryMovieDeleteList has any values
            //If it has delete them from the database
            if (categoryMovieDeleteList.Any())
            {
                unitOfWork.CategoryMovie.DeleteRange(categoryMovieDeleteList);
            }

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

        /// <summary>
        /// Create new <see cref="CategoryMovie"/>s that are going to be created.
        /// </summary>
        /// <param name="movieId">Id of the <see cref="Movie"/></param>
        /// <param name="categoryIdList">Category Id List</param>
        /// <returns>A set list of <see cref="CategoryMovie"/>s.</returns>
        private List<CategoryMovie> FillCreateCategoryMovieList(Guid movieId, List<Guid> categoryIdList)
        {
            List<CategoryMovie> categoryMovieCreateList = new List<CategoryMovie>();

            foreach (Guid categoryId in categoryIdList)
            {
                CategoryMovie categoryMovie = new CategoryMovie()
                {
                    MovieId = movieId,
                    CategoryId = categoryId
                };

                categoryMovieCreateList.Add(categoryMovie);
            }

            return categoryMovieCreateList;
        }
    }
}