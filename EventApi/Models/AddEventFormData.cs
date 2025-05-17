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
        public string CategoryName { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public string StatusName { get; set; } = null!;
    }
}
