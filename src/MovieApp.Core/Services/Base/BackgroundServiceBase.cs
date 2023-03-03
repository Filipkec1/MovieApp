using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MovieApp.Infrastructure.EfUnitsOfWork;

namespace MovieApp.Core.Services.Base
{
    /// <summary>
    /// Defines background service base class.
    /// </summary>
    public abstract class BackgroundServiceBase : BackgroundService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundServiceBase"/> class.
        /// </summary>
        /// <param name="logger">For logging</param>
        /// <param name="serviceProvider">To get dependency injection things like <see cref="IUnitOfWork"/> or something else</param>
        public BackgroundServiceBase(IServiceProvider provider)
        {
            serviceProvider = provider;
        }

        /// <summary>
        /// Gets or sets <see cref="IServiceProvider"/>.
        /// </summary>
        protected IServiceProvider serviceProvider;
    }
}