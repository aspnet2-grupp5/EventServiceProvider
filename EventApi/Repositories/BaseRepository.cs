using EventApi.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EventApi.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(string id);
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> AddAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteByIdAsync(Expression<Func<TEntity, bool>> expression);
    }

    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly EventsDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        public BaseRepository(EventsDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public virtual async Task<TEntity?> GetByIdAsync(string id)
        {
            return await _dbSet.FindAsync(id);
        }
        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
        public virtual async Task<bool> AddAsync(TEntity entity)
        {
            if (entity == null)
                return false;

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            if (entity == null)
                return false;

            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        public virtual async Task<bool> DeleteByIdAsync(Expression<Func<TEntity, bool>> expression)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(expression);
            if (entity == null)
                return false;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

