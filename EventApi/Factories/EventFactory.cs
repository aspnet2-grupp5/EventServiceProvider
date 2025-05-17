using EventApi.Data.Entities;
using EventApi.Models;

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
                Description = entity.Description,
                Date = entity.Date,
                Price = entity.Price,
                Quantity = entity.Quantity,
                SoldQuantity = entity.SoldQuantity,

                Category = new Category
                {
                    CategoryId = entity.Category.CategoryId,
                    CategoryName = entity.Category.CategoryName
                },
                Location = new Location
                {
                    LocationId = entity.Location.LocationId,
                    Address = entity.Location.Address
                },
                Status = new Status
                {
                    StatusId = entity.Status.StatusId,
                    StatusName = entity.Status.StatusName
                }
            };
        }
        public static EventEntity ToEntity(AddEventFormData formData)
        {
            return formData == null
                ? null!
                : new EventEntity
                {
                    EventId = formData.EventId,
                    EventTitle = formData.EventTitle,
                    Description = formData.Description,
                    Date = formData.Date,
                    Price = formData.Price,
                    Quantity = formData.Quantity,
                    SoldQuantity = 0,
                    CategoryId = formData.CategoryName,
                    LocationId = formData.Address,
                    StatusId = formData.StatusName,
                };
        }
        public static EventEntity ToEntity(EditEventformData formdata)
        {
            return formdata == null
                ? null!
                : new EventEntity
                {
                    EventId = formdata.EventId,
                    EventTitle = formdata.EventTitle,
                    Description = formdata.Description,
                    Date = formdata.Date,
                    Price = formdata.Price,
                    Quantity = formdata.Quantity,
                    SoldQuantity = 0,
                    CategoryId = formdata.CategoryName,
                    LocationId = formdata.Address,
                    StatusId = formdata.StatusName,
                };
        }
    }

}
