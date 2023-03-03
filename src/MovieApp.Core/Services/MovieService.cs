using Microsoft.Extensions.Logging;
using MovieApp.Core.Models.Entities;
using MovieApp.Core.Services.Base;
using MovieApp.Infrastructure.EfUnitsOfWork;

namespace MovieApp.Core.Services
{
    /// <summary>
    /// Defines <see cref="Movie"/> Service interface.
    /// </summary>
    public interface IMovieService
    { }

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
    }
}