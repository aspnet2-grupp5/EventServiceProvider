using EventApi.Data.Entities;
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
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StatusEntity>().HasData(
                new StatusEntity { StatusId = "1", StatusName = "Active" },
                new StatusEntity { StatusId = "2", StatusName = "Past" },
                new StatusEntity { StatusId = "3", StatusName = "Draft" }
            );
        }
    }
    }
