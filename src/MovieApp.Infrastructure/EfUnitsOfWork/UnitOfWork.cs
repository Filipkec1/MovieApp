using MovieApp.Core.Repositories;
using MovieApp.Core.Repositories.Base;
using MovieApp.Infrastructure.Context;
using MovieApp.Infrastructure.EfRepository;

namespace MovieApp.Infrastructure.EfUnitsOfWork
{
    /// <summary>
    /// Defines unit of work.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MovieAppContext context;

        private IRoleRepository roleRepository;
        private IUserRepository userRepository;

        public UnitOfWork(MovieAppContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task Commit()
        {
            await context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public IRoleRepository Role
        {
            get
            {
                if (roleRepository is null)
                {
                    roleRepository = new RoleRepository(context);
                }

                return roleRepository;
            }
        }

        /// <inheritdoc/>
        public IUserRepository User
        {
            get
            {
                if(userRepository is null)
                {
                    userRepository = new UserRepository(context);
                }

                return userRepository;
            }
        }
    }
}