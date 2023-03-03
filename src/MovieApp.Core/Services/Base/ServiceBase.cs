using Microsoft.Extensions.Logging;
using MovieApp.Infrastructure.EfUnitsOfWork;

namespace MovieApp.Core.Services.Base
{
    /// <summary>
    /// Defines service base class.
    /// </summary>
    /// <typeparam name="T">The interface of the service.</typeparam>
    public abstract class ServiceBase<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBase{T}"/> class.
        /// </summary>
        /// <param name="unitOfWork"><see cref="IUnitOfWork"/></param>
        public ServiceBase(IUnitOfWork unitOfWork, ILogger<T> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Gets or sets <see cref="ILogger{T}"/>.
        /// </summary>
        protected ILogger<T> logger { get; set; }

        /// <summary>
        /// Gets or sets <see cref="IUnitOfWork"/>.
        /// </summary>
        protected IUnitOfWork unitOfWork { get; set; }
    }
}