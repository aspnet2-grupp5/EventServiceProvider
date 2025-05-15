using EventApi.Data.Contexts;
using EventApi.Data.Entities;
using EventApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EventApi.Services
{
    public interface IEventService
    {
        Task<EventEntity?> CreateEventAsync(AddEventFormData formData);
        Task<bool> UpdateEventAsync(string id, EditEventformData formData);
        Task<EventEntity?> GetEventByIdAsync(string id);
        Task<bool> DeleteEventAsync(string id);
    }

    public class EventService : IEventService
    {
        private readonly EventsDbContext _context;

        public EventService(EventsDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateEventAsync(AddEventFormData formData)
        {
            var newEvent = new EventEntity
            {
                EventId = Guid.NewGuid().ToString(),
                EventTitle = formData.EventTitle,
                Description = formData.Description,
                Date = formData.Date,
                Price = formData.Price,
                Quantity = formData.Quantity,
                SoldQuantity = 0,
                CategoryId = formData.CategoryId,
                LocationId = formData.LocationId,
                StatusId = formData.StatusId
            };

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateEventAsync(string id, EditEventformData formData)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return false;

            ev.EventTitle = formData.EventTitle;
            ev.Description = formData.Description;
            ev.Date = formData.Date;
            ev.Price = formData.Price;
            ev.Quantity = formData.Quantity;
            ev.CategoryId = formData.CategoryId;
            ev.LocationId = formData.LocationId;
            ev.StatusId = formData.StatusId;

            _context.Events.Update(ev);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEventAsync(string id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) 
                return false;

            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<EventEntity> GetEventByIdAsync(string id)
        {
            return await _context.Events
                .Include(e => e.Category)
                .Include(e => e.Location)
                .Include(e => e.Status)
                .FirstOrDefaultAsync(e => e.EventId == id);
        }

        public async Task<IEnumerable<EventEntity>> GetAllEventsAsync()
        {
            return await _context.Events
                .Include(e => e.Category)
                .Include(e => e.Location)
                .Include(e => e.Status)
                .ToListAsync();
        }
    }
}
