using Microsoft.EntityFrameworkCore;
using MovieApp.Core.Models.Entities;
using MovieApp.Core.Repositories;
using MovieApp.Infrastructure.Context;
using MovieApp.Infrastructure.EfRepository.Base;

namespace MovieApp.Infrastructure.EfRepository
{
    /// <summary>
    /// Defines repository for <see cref="Role"/>.
    /// </summary>
    public class RoleRepository : RepositoryBase<Role, Guid>, IRoleRepository
    {
        /// <summary>
        /// Initialize new instance of the <see cref="RoleRepository"/> class.
        /// </summary>
        /// <param name="context"><see cref="MovieAppContext"/></param>
        public RoleRepository(MovieAppContext context) : base(context)
        { }

        /// <inheritdoc />
        public override async Task<Role?> GetById(Guid id)
        {
            return await GetTableQueryable()
                        .AsNoTracking()
                        .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}