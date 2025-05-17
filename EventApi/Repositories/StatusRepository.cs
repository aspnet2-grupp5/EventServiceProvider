using EventApi.Data.Contexts;
using EventApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventApi.Repositories
{
    public interface IStatusRepository
    {
        Task<IEnumerable<StatusEntity>> GetAllAsync();
        Task<StatusEntity?> GetByIdAsync(string id);
        Task<StatusEntity?> GetByNameAsync(string name);
    }

    public class StatusRepository : IStatusRepository
    {
        private readonly EventsDbContext _context;
        private readonly DbSet<StatusEntity> _dbSet;

        public StatusRepository(EventsDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<StatusEntity>();
        }

        public async Task<IEnumerable<StatusEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<StatusEntity?> GetByIdAsync(string id)
        {
            return await _dbSet.FirstOrDefaultAsync(s => s.StatusId == id);
        }

        public async Task<StatusEntity?> GetByNameAsync(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(s => s.StatusName.ToLower() == name.ToLower());
        }
    }
}
