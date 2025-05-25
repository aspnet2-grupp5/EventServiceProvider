namespace EventApi.Models
{
    public class EventModel
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
        public CategoryModel? Category { get; internal set; }
        public LocationModel? Location { get; internal set; }
        public StatusModel? Status { get; internal set; }
    }
}
