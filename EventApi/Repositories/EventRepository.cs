using EventApi.Data.Contexts;
using EventApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

public class EventRepository : IEventRepository
{
    private readonly EventsDbContext _context;

    public EventRepository(EventsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EventEntity>> GetAllEventsAsync()
    {
        return await _context.Events
            .Include(e => e.Category)
            .Include(e => e.Location)
            .Include(e => e.Status)
            .ToListAsync();
    }

    public async Task<IEnumerable<EventEntity>> GetEventsByCategoryIdAsync(int categoryId)
    {
        return await _context.Events
            .Where(e => e.CategoryId == categoryId)
            .Include(e => e.Category)
            .ToListAsync();
    }

    public async Task<IEnumerable<EventEntity>> GetEventsByLocationIdAsync(int locationId)
    {
        return await _context.Events
            .Where(e => e.LocationId == locationId)
            .Include(e => e.Location)
            .ToListAsync();
    }

    public async Task<IEnumerable<EventEntity>> GetEventsByStatusIdAsync(int statusId)
    {
        return await _context.Events
            .Where(e => e.StatusId == statusId)
            .Include(e => e.Status)
            .ToListAsync();
    }

    public async Task<IEnumerable<EventEntity>> GetEventsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Events
            .Where(e => e.Date >= startDate.Date && e.Date <= endDate.Date)
            .ToListAsync();
    }
}
