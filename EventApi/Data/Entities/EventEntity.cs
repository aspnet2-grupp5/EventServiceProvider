using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventApi.Data.Entities
{
    public class EventEntity
    {
        [Key]
        public string EventId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string EventTitle { get; set; } = null!;
        public string? Image {  get; set; } 

        public string Description { get; set; } = null!;

        public DateTime? Date { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        public int? Quantity { get; set; }
        public int? SoldQuantity { get; set; }

        [ForeignKey(nameof(Category))]
        public string? CategoryId { get; set; } 
        public  CategoryEntity? Category { get; set; }
        [ForeignKey(nameof(Location))]
        public string? LocationId { get; set; } 
        public  LocationEntity? Location { get; set; } 
        [ForeignKey(nameof(Status))]
        public string? StatusId { get; set; }
        public StatusEntity? Status { get; set; } 
    }
}