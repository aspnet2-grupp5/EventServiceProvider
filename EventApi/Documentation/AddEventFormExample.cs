using Swashbuckle.AspNetCore.Filters;
using EventApi.Models;

namespace EventApi.Documentation
{
    public class AddEventFormExample : IExamplesProvider<AddEventFormData>
    {
        public AddEventFormData GetExamples()
        {
            return new AddEventFormData
            {
                EventTitle = "Sample Event",
                Description = "This is a sample event description.",
                Date = DateTime.UtcNow.AddDays(30),
                Price = 50.00m,
                Quantity = 100,
                CategoryName = "Health",
                Address = "Solnavägen 5B",
                StatusName = "Past",
            };
        }
    }
}
