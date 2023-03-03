using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MovieApp.Core.Helpers;
using MovieApp.Core.Models.Entities;
using MovieApp.Core.Request;
using MovieApp.Core.Results;
using MovieApp.Core.Results.Base;
using MovieApp.Core.Services.Base;
using MovieApp.Core.Settings;
using MovieApp.Infrastructure.EfUnitsOfWork;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieApp.Core.Services
{
    /// <summary>
    /// Defines <see cref="User"/> Service interface.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Get all <see cref="User"/>s with their <see cref="Role"/>
        /// </summary>
        /// <returns>A list of <see cref="User"/>s as <see cref="UserResult"/></returns>
        Task<Result<IEnumerable<UserResult>>> GetAllUsers();

        /// <summary>
        /// Get <see cref="User"/> by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A <see cref="User"/> as <see cref="UserResult"/></returns>
        Task<Result<UserResult>> GetUserById(Guid id);

        /// <summary>
        /// Login <see cref="User"/>
        /// </summary>
        /// <param name="request"><see cref="UserLoginRequest"/></param>
        /// <returns>A valid JWT.</returns>
        Task<Result<string>> Login(UserLoginRequest request);
    }

    /// <summary>
    /// Defines <see cref="User"/> service.
    /// </summary>
    public class UserService : ServiceBase<IUserService>, IUserService
    {
        private readonly JWTSettings jWTSettings;
        private readonly IPasswordHasher passwordHasher;

        /// <summary>
        /// Initilizes new instance of <see cref="UserService"/>
        /// </summary>
        /// <param name="unitOfWork">Unit of work.</param>
        /// <param name="logger">Logger.</param>
        /// <param name="logger"><see cref="IPasswordHasher"/></param>
        public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger,
                           IPasswordHasher hasher, JWTSettings settings) : base(unitOfWork, logger)
        {
            passwordHasher = hasher;
            jWTSettings = settings;
        }

        /// <inheritdoc/>
        public async Task<Result<IEnumerable<UserResult>>> GetAllUsers()
        {
            IEnumerable<User> userList = await unitOfWork.User.GetAllUsers();
            return userList.Select(u => new UserResult(u)).ToList();
        }

        /// <inheritdoc/>
        public async Task<Result<UserResult>> GetUserById(Guid id)
        {
            User? user = await unitOfWork.User.GetById(id);
            if (user == null)
            {
                return Result.Failure<UserResult>(new Error("401", $"Username or password is incorrect."));
            }

            return new UserResult(user);
        }

        /// <inheritdoc/>
        public async Task<Result<string>> Login(UserLoginRequest request)
        {
            //Check if the user exists
            User? user = await unitOfWork.User.GetUserWithRoleByName(request.Username);
            if (user == null)
            {
                return Result.Failure<string>(new Error("404", $"User with name {request.Username} does not exist."));
            }

            if (!passwordHasher.VerifyPassword(request.Password, user.Hash))
            {
                return Result.Failure<string>(new Error("401", $"Username or password is incorrect."));
            }

            return GenerateJwt(user);
        }

        /// <summary>
        /// Generate a JWT for a <see cref="User"/>
        /// </summary>
        /// <param name="user"><see cref="User"/></param>
        /// <returns>A JWT.</returns>
        private string GenerateJwt(User user)
        {
            // generate token that is valid for 8 hours
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(jWTSettings.Secret);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("Id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}