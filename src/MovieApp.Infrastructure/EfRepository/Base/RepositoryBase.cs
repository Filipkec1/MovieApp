using Microsoft.EntityFrameworkCore;
using MovieApp.Core.Repositories.Base;
using MovieApp.Infrastructure.Context;

namespace MovieApp.Infrastructure.EfRepository.Base
{
    /// <summary>
    /// Defines repository base class.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class RepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : class
    {
        public RepositoryBase(MovieAppContext context)
        {
            this.context = context;
        }

        protected MovieAppContext context { get; }

        /// <inheritdoc />
        public virtual async Task<TEntity> Add(TEntity entity)
        {
            await GetTable().AddAsync(entity);

            return entity;
        }

        /// <inheritdoc />
        public virtual async Task AddRange(IEnumerable<TEntity> entities)
        {
            await GetTable().AddRangeAsync(entities);
        }

        /// <inheritdoc />
        public virtual void Delete(TEntity entity)
        {
            GetTable().Remove(entity);
        }

        /// <inheritdoc />
        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            GetTable().RemoveRange(entities);
        }

        /// <inheritdoc />
        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await GetTableQueryable()
                            .AsNoTracking()
                            .ToListAsync();
        }

        /// <inheritdoc />
        public abstract Task<TEntity?> GetById(TPrimaryKey id);

        /// <inheritdoc />
        public void Update(TEntity entity)
        {
            GetTable().Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            GetTable().UpdateRange(entities);
        }

        protected DbSet<TEntity> GetTable()
        {
            return context.Set<TEntity>();
        }

        protected virtual IQueryable<TEntity> GetTableQueryable()
        {
            return GetTable();
        }
    }
}