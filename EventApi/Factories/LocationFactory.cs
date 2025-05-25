using EventApi.Data.Entities;
using EventApi.Protos;

namespace EventApi.Factories
{
    public class LocationFactory
    {
        public static Location ToModel(LocationEntity locationEntity)
        {
            if (locationEntity == null) return null!;

            return new Location
            {
                LocationId = locationEntity.LocationId,
                Address = locationEntity.Address,
                City = locationEntity.City,
            };

    }
    }
}

