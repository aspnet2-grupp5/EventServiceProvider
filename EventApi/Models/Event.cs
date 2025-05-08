using System.ComponentModel.DataAnnotations;

namespace EventApi.Models
{
    public class Event
    {
        public string EventId { get; set; } = null;
        public string Title { get; set; } = null!;
        public string Category { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Location { get; set; } = null!;
        public string Status { get; set; } = null!;
        public decimal Price { get; set; }
        public  int BookingPercentage { get; set; }
    }
}
