using EventApi.Data.Contexts;
using EventApi.Data.Entities;

namespace EventApi.Repositories
{
    public interface  ILocationRepository : IBaseRepository<LocationEntity>
    {
    }

    public class LocationRepository (EventsDbContext context) : BaseRepository<LocationEntity>(context), ILocationRepository 
    {
    }
}