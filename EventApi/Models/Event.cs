using System.ComponentModel.DataAnnotations.Schema;

namespace EventApi.Models
{
    public class Event
    {
        public string EventId { get; set; } = null!;
        public string EventTitle { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int SoldQuantity { get; set; }
        public Category Category { get; set; } = null!;
        public Location Location { get; set; } = null!;
        public Status Status { get; set; } = null!;
    }
    
}
