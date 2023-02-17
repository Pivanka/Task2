using DAL.Context;
using DAL.Models;
using DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly LibraryDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id) ?? throw new ArgumentException($"Entity {typeof(TEntity)} with this id={id} not found!");
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            CheckNull(entity);
            return (await _dbSet.AddAsync(entity)).Entity;
        }

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes)
        {
            var dbSet = _dbContext.Set<TEntity>();
            var query = includes
                .Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>(dbSet, (current, include) => current.Include(include));

            return query ?? dbSet;
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                if (_dbContext.Database.CurrentTransaction != null)
                {
                    await _dbContext.Database.CurrentTransaction.RollbackAsync();
                }

                throw;
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            return await Task.Run(() => _dbSet.Update(entity).Entity);
        }

        private static void CheckNull(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "The entity to add cannot be null.");
            }
        }

        public async Task<TEntity> RemoveAsync(TEntity entity)
        {
            CheckNull(entity);
            return await Task.Run(() => _dbSet.Remove(entity).Entity);
        }
    }
}
