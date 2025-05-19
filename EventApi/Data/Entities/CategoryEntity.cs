using System.ComponentModel.DataAnnotations;

namespace EventApi.Data.Entities
{
    public class CategoryEntity
    {
        [Key]
        public string CategoryId { get; set; } = null!;

        [Required]
        public string CategoryName { get; set; } = null!;

        public ICollection<EventEntity> Events { get; set; } = new List<EventEntity>();
    }
}
