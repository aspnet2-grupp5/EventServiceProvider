using EventApi.Entities;
using EventApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EventApi.Data.Contexts
{
    public class EventsDbContext(DbContextOptions<EventsDbContext> options) : DbContext(options)
    {
        public DbSet<EventEntity> Events { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<LocationEntity> Locations { get; set; }
        public DbSet<StatusEntity> Statuses { get; set; }
        public DbSet<MemberEntity> Members { get; set; }
        public DbSet<AdminEntity> Admins { get; set; }
    }
    
}
