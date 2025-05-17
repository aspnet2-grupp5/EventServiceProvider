using EventApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace EventApi.Documentation
{
    public class EditEventFormExampel : IExamplesProvider<EditEventformData>
    {
        public EditEventformData GetExamples()
        {
            return new EditEventformData
            {
                EventId = "12345",
                EventTitle = "Updated Event Title",
                Description = "Updated event description.",
                Date = DateTime.UtcNow.AddDays(30),
                Price = 75.00m,
                Quantity = 50,
                CategoryName = "Music",
                Address = "Globen",
                StatusName= "Active",
            };
        }
    }
    }

