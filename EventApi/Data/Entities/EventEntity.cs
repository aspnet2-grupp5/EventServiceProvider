using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EventApi.Data.Entities
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

        [ForeignKey(nameof(Category))]
        public string CategoryId { get; set; } = null!;
        public virtual CategoryEntity Category { get; set; } = null!;
        [ForeignKey(nameof(Location))]
        public string LocationId { get; set; } =null!;
        public virtual LocationEntity Location { get; set; } = null!;
        [ForeignKey(nameof(Status))]
        public string StatusId { get; set; } = null!;
        public virtual StatusEntity Status { get; set; } = null!;
    }
}
