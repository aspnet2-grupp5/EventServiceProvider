using System.ComponentModel.DataAnnotations;

namespace EventApi.Models
{
    public class AddEventFormData
    {
        [Required]
        public string EventTitle { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int LocationId { get; set; }
        [Required]
        public int StatusId { get; set; }
    }
}
