namespace MovieApp.Infrastructure.EfUnitsOfWork
{
    /// <summary>
    /// Defines interface for UnitOfWork.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Used to commit changes to the underlaying persistence layer
        /// </summary>
        /// <returns></returns>
        Task Commit();
    }
}