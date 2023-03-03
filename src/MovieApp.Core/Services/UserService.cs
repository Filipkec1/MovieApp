using Microsoft.Extensions.Logging;
using MovieApp.Core.Models.Entities;
using MovieApp.Core.Services.Base;
using MovieApp.Infrastructure.EfUnitsOfWork;

namespace MovieApp.Core.Services
{
    /// <summary>
    /// Defines <see cref="User"/> Service interface.
    /// </summary>
    public interface IUserController
    {

    }

    /// <summary>
    /// Defines <see cref="User"/> service.
    /// </summary>
    public class UserService : ServiceBase<IUserController>, IUserController
    {
        /// <summary>
        /// Initilizes new instance of <see cref="HeistService"/>
        /// </summary>
        /// <param name="unitOfWork">Unit of work.</param>
        /// <param name="logger">Logger.</param>
        public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger) : base(unitOfWork, logger)
        { }
    }
}