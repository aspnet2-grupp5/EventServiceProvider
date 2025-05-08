using EventApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EventApi.Data.Contexts
{
    public class EventsDbContext(DbContextOptions<EventsDbContext> options) : DbContext(options)
    {
        public DbSet<Event> Events { get; set; }
    }
    
}
