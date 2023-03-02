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
        public BackgroundServiceBase(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        /// <summary>
        /// Gets or sets <see cref="ILogger"/>.
        /// </summary>
        protected IServiceProvider serviceProvider;
    }
}