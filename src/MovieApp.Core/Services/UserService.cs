using Microsoft.Extensions.Logging;
using MovieApp.Core.Models.Entities;
using MovieApp.Core.Results;
using MovieApp.Core.Results.Base;
using MovieApp.Core.Services.Base;
using MovieApp.Infrastructure.EfUnitsOfWork;

namespace MovieApp.Core.Services
{
    /// <summary>
    /// Defines <see cref="User"/> Service interface.
    /// </summary>
    public interface IUserService
    {
        Task<Result<IEnumerable<UserResult>>> GetAllUsers();
    }

    /// <summary>
    /// Defines <see cref="User"/> service.
    /// </summary>
    public class UserService : ServiceBase<IUserService>, IUserService
    {
        /// <summary>
        /// Initilizes new instance of <see cref="HeistService"/>
        /// </summary>
        /// <param name="unitOfWork">Unit of work.</param>
        /// <param name="logger">Logger.</param>
        public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger) : base(unitOfWork, logger)
        { }

        /// <inheritdoc/>
        public async Task<Result<IEnumerable<UserResult>>> GetAllUsers()
        {
            IEnumerable<User> userList = await unitOfWork.User.GetAllUsers();
            return userList.Select(u => new UserResult(u)).ToList();
        }
    }
}