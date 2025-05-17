using EventApi.Data.Contexts;
using EventApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EventApi.Repositories;
public interface IEventRepository
{
    Task<IEnumerable<EventEntity>> GetAllEventsAsync();
    Task<EventEntity?> GetByIdAsync(string id);
    Task<IEnumerable<EventEntity>> GetAsync(Expression<Func<EventEntity, bool>> predicate);
    Task<IEnumerable<EventEntity>> GetByCategoryIdAsync(string categoryId);
    Task<IEnumerable<EventEntity>> GetByLocationIdAsync(string locationId);
    Task<IEnumerable<EventEntity>> GetByStatusNameAsync(string statusName);
    Task <bool> AddAsync(EventEntity entity);
    Task<bool> UpdateAsync(EventEntity entity);
    Task<bool> DeleteByIdAsync(string id);
}
public class EventRepository : IEventRepository
{
    private readonly EventsDbContext _context;
    private readonly DbSet<EventEntity> _dbSet;
    public EventRepository(EventsDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<EventEntity>();
    }

    public async Task<IEnumerable<EventEntity>> GetAllEventsAsync()
    {
        return await _dbSet
            .Include(e => e.Category)
            .Include(e => e.Location)
            .Include(e => e.Status)
            .ToListAsync();
    }

    public async Task<EventEntity?> GetByIdAsync(string id)
    {
        return await _dbSet
            .Include(e => e.Category)
            .Include(e => e.Location)
            .Include(e => e.Status)
            .FirstOrDefaultAsync(e => e.EventId == id);
    }
    public async Task<IEnumerable<EventEntity>> GetAsync(Expression<Func<EventEntity, bool>> predicate)
    {
        return await _dbSet
            .Where(predicate)
            .Include(e => e.Category)
            .Include(e => e.Location)
            .Include(e => e.Status)
            .ToListAsync();
    }
    public Task<IEnumerable<EventEntity>> GetByCategoryIdAsync(string categoryId)
    {
        return GetAsync(e => e.CategoryId == categoryId);
    }
    public Task<IEnumerable<EventEntity>> GetByLocationIdAsync(string locationId)
    {
        return GetAsync(e => e.LocationId == locationId);
    }
    public async Task<IEnumerable<EventEntity>> GetByStatusNameAsync(string statusName)
    {
        return await _dbSet
            .Include(e => e.Category)
            .Include(e => e.Location)
            .Include(e => e.Status)
            .Where(e => e.Status.StatusName.ToLower() == statusName.ToLower())
            .ToListAsync();
    }
    public async Task<bool> AddAsync(EventEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> UpdateAsync(EventEntity entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> DeleteByIdAsync(string id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
            return false;

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}