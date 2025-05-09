using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EventApi.Entities
{
    public class EventEntity
    {
        [Key]
        public string EventId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string EventTitle { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }
        public int SoldQuantity { get; set; }

        public int CategoryId { get; set; }
        public CategoryEntity Category { get; set; } = null!;

        public int LocationId { get; set; }
        public LocationEntity Location { get; set; } = null!;

        public int StatusId { get; set; }
        public StatusEntity Status { get; set; } = null!;
    }
}
