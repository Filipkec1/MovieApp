using Microsoft.Extensions.Logging;
using Moq;
using MovieApp.Core.Request;
using MovieApp.Core.Results;
using MovieApp.Core.Results.Base;
using MovieApp.Core.Services;
using MovieApp.Infrastructure.EfUnitsOfWork;
using System.Text.Json;

namespace MovieApp.UnitTest
{
    /// <summary>
    /// Defines <see cref="MovieService"/> unit test.
    /// </summary>
    public class MovieServiceTest
    {
        private readonly Mock<ILogger<IMovieService>> logger;
        private readonly MovieService service;
        private readonly Mock<IUnitOfWork> unitOfWork;

        private Category category = new Category {Id = Guid.Parse("ed58ed3f-f7e1-46b1-95d6-1507505359bd"), Name = "Test"};

        /// <summary>
        /// Initilizes new instance of <see cref="MovieServiceTest"/>
        /// </summary>
        public MovieServiceTest()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            logger = new Mock<ILogger<IMovieService>>();

            service = new MovieService(unitOfWork.Object, logger.Object);
        }

        [Fact]
        public async void CreateMovie_ShouldCreateMovie_ReturnMovieResult()
        {
            //Arrange
            //Movie
            Movie movie = new Movie() { Id = Guid.Parse("c2fa2d29-c543-40b1-b2b0-af335e5c9363"), Title = "Test" };

            //CategoryMovie
            CategoryMovie categoryMovie = new CategoryMovie() {Id = Guid.Parse("26a728c2-d1ae-4f6b-b3c6-8a3e3a7a6b30"), Category = category, CategoryId = category.Id, MovieId = movie.Id, Movie = movie};

            movie.CategoryMovie.Add(categoryMovie);

            //MovieCreateRequest
            MovieCreateRequest movieCreateRequest = new MovieCreateRequest();
            movieCreateRequest.Title = "Test";
            movieCreateRequest.CategoryIdList.Add(category.Id);

            //Unit of work
            unitOfWork.Setup(x => x.Movie.Add(It.IsAny<Movie>()));
            unitOfWork.Setup(x => x.CategoryMovie.AddRange(It.IsAny<IEnumerable<CategoryMovie>>()));
            unitOfWork.Setup(x => x.Commit());
            unitOfWork.Setup(x => x.Movie.GetMovieAndCategoryWithMovieId(It.IsAny<Guid>())).ReturnsAsync(movie);

            //Act
            Result<MovieResult> result = await service.CreateMovie(movieCreateRequest);

            //Assert
            MovieResult movieResult = new MovieResult(movie);
            string expectedResult = JsonSerializer.Serialize(movieResult);

            string serviceResult = JsonSerializer.Serialize(result.Value);

            Assert.Equal(expectedResult, serviceResult);
        }

        [Fact]
        public async void CreateMovie_ShouldFail_ReturnErrorCategoryIsRepeted()
        {
            //Arrange
            //MovieCreateRequest
            MovieCreateRequest movieCreateRequest = new MovieCreateRequest();
            movieCreateRequest.Title = "Test";
            movieCreateRequest.CategoryIdList.Add(category.Id);
            movieCreateRequest.CategoryIdList.Add(category.Id);

            //Unit of work
            unitOfWork.Setup(x => x.Movie.Add(It.IsAny<Movie>()));

            //Act
            Result<MovieResult> result = await service.CreateMovie(movieCreateRequest);

            //Assert
            Error error = new Error("400", "A category is repeted.");
            string expectedResult = JsonSerializer.Serialize(error);

            string serviceResult = JsonSerializer.Serialize(result.Error);

            Assert.Equal(expectedResult, serviceResult);
        }
    }
}