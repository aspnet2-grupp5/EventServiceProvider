using System.ComponentModel.DataAnnotations;

namespace EventApi.Entities
{
    public class StatusEntity
    {
        [Key]
        public string StatusId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; set; } = null!;

        public ICollection<EventEntity> Events { get; set; } = new List<EventEntity>();
    }

}
