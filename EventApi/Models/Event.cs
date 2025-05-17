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

        public string CategoryName { get; set; } = null!;
        public string LocationName { get; set; } = null!;
        public string StatusName { get; set; } = null!;
        public Category? Category { get; internal set; }
        public Location? Location { get; internal set; }
        public Status? Status { get; internal set; }
    }
}
