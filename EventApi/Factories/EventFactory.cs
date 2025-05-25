using EventApi.Data.Entities;
using EventApi.Protos;
using Google.Protobuf.WellKnownTypes;

namespace EventApi.Factories
{
    public class EventFactory
    {
        public static Event ToModel(EventEntity entity)
        {
            if (entity == null) return null!;
            return new Event
            {
                EventId = entity.EventId,
                EventTitle = entity.EventTitle,
                EventImage = entity.Image ?? string.Empty,
                Description = entity.Description,
                Date = entity.Date.HasValue ? Timestamp.FromDateTime(entity.Date.Value.ToUniversalTime()) : new Timestamp(),
                Price = entity.Price.HasValue ? (double)entity.Price.Value : 0.0,
                Quantity = entity.Quantity ?? 0,
                SoldQuantity = entity.SoldQuantity ?? 0,

                Category = new Category
                {
                    CategoryId = entity.Category?.CategoryId ?? string.Empty,
                    CategoryName = entity.Category?.CategoryName ?? string.Empty,
                },
                Location = new Location
                {
                    LocationId = entity.Location?.LocationId ?? string.Empty,
                    Address = entity.Location?.Address ?? string.Empty,
                    City = entity.Location?.City ?? string.Empty,

                },
                Status = new Status
                {
                    StatusId = entity.Status?.StatusId ?? string.Empty,
                    StatusName = entity.Status?.StatusName ?? string.Empty,
                }
            };
        }
        public static EventEntity ToEntity(Event eventRequest)
        {
            return eventRequest == null
                ? null!
                : new EventEntity
                {
                    EventId = eventRequest.EventId,
                    EventTitle = eventRequest.EventTitle,
                    Image = eventRequest.EventImage ?? string.Empty,
                    Description = eventRequest.Description,
                    Date = eventRequest.Date.ToDateTime(),
                    Price = (decimal) eventRequest.Price,
                    Quantity = eventRequest.Quantity,
                    SoldQuantity = 0,
                    CategoryId = eventRequest.Category.CategoryId,
                    LocationId = eventRequest.Location.LocationId,
                    StatusId = eventRequest.Status.StatusId,
                };
        }
    }

}
