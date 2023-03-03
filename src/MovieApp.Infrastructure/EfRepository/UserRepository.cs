using Microsoft.EntityFrameworkCore;
using MovieApp.Core.Models.Entities;
using MovieApp.Core.Repositories;
using MovieApp.Infrastructure.Context;
using MovieApp.Infrastructure.EfRepository.Base;

namespace MovieApp.Infrastructure.EfRepository
{
    /// <summary>
    /// Defines repository for <see cref="User"/>.
    /// </summary>
    public class UserRepository : RepositoryBase<User, Guid>, IUserRepository
    {
        /// <summary>
        /// Initialize new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context"><see cref="MovieAppContext"/></param>
        public UserRepository(MovieAppContext context) : base(context)
        { }

        /// <inheritdoc />
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await GetTableQueryable()
                        .AsNoTracking()
                        .Include(u => u.Role)
                        .ToListAsync();
        }

        /// <inheritdoc />
        public override async Task<User?> GetById(Guid id)
        {
            return await GetTableQueryable()
                        .AsNoTracking()
                        .Include(u => u.Role)
                        .FirstOrDefaultAsync(u => u.Id == id);
        }

        /// <inheritdoc />
        public async Task<User?> GetUserWithRoleByName(string name)
        {
            return await GetTableQueryable()
                        .AsNoTracking()
                        .Include(u => u.Role)
                        .FirstOrDefaultAsync(u => u.Name == name);
        }
    }
}