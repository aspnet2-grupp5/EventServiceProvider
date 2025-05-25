using EventApi.Data.Entities;
using EventApi.Protos;

namespace EventApi.Factories
{
    public class StatusFactory
    {
        public static Status ToModel(StatusEntity statusEntity)
        {
            if (statusEntity == null) return null!;
            return new Status
            {
                StatusId = statusEntity.StatusId,
                StatusName = statusEntity.StatusName,

            };

        }
    }
}
