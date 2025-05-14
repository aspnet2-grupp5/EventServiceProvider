using System.ComponentModel.DataAnnotations;

namespace EventApi.Entities
{
    public class CategoryEntity
    {
        [Key]
        public string CategoryId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Category { get; set; } = null!;

        public ICollection<EventEntity> Events { get; set; } = new List<EventEntity>();
    }
}
