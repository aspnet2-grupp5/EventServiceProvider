using System.ComponentModel.DataAnnotations;

namespace EventApi.Entities
{
    public class LocationEntity
    {
        [Key]
        public string LocationId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public ICollection<EventEntity> Events { get; set; } = new List<EventEntity>();
    }

}
