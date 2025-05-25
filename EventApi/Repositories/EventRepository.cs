using EventApi.Data.Contexts;
using EventApi.Data.Entities;

namespace EventApi.Repositories;
public interface IEventRepository : IBaseRepository<EventEntity>
{
    Task<IEnumerable<EventEntity>> GetByCategoryIdAsync(string categoryId);
    Task<IEnumerable<EventEntity>> GetByLocationIdAsync(string locationId);
    Task<IEnumerable<EventEntity>> GetByStatusNameAsync(string statusName);
}
public class EventRepository : BaseRepository<EventEntity>, IEventRepository
{
    public EventRepository(EventsDbContext context) : base(context) { }

    public Task<IEnumerable<EventEntity>> GetByCategoryIdAsync(string categoryId)
    {
        return GetAllAsync(
            filterBy: e => e.CategoryId == categoryId,
            includes: [e => e.Category, e => e.Location, e => e.Status]);
    }

    public Task<IEnumerable<EventEntity>> GetByLocationIdAsync(string locationId)
    {
        return GetAllAsync(
            filterBy: e => e.LocationId == locationId,
            includes: [e => e.Category, e => e.Location, e => e.Status]);
    }

    public Task<IEnumerable<EventEntity>> GetByStatusNameAsync(string statusName)
    {
        return GetAllAsync(
            filterBy: e => e.Status.StatusName.ToLower() == statusName.ToLower(),
            includes: [e => e.Category, e => e.Location, e => e.Status]);
    }
}
