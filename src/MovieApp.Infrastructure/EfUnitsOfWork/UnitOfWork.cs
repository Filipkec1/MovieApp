using MovieApp.Core.Repositories.Base;
using MovieApp.Infrastructure.Context;

namespace MovieApp.Infrastructure.EfUnitsOfWork
{
    /// <summary>
    /// Defines unit of work.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MovieAppContext context;

        public UnitOfWork(MovieAppContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public async Task Commit()
        {
            await context.SaveChangesAsync();
        }
    }
}