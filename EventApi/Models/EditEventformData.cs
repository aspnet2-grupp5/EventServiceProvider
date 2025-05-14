using System.ComponentModel.DataAnnotations;

namespace EventApi.Models
{
    public class EditEventformData
    {
        public string EventId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string EventTitle { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public int LocationId { get; set; }
        public int StatusId { get; set; }
    }
}
