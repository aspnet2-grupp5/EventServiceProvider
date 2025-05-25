using System.ComponentModel.DataAnnotations;

namespace EventApi.Data.Entities
{
    public class LocationEntity
    {
        [Key]
        public string LocationId { get; set; } = null!;

        [Required]

        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;

        public ICollection<EventEntity> Events { get; set; } = new List<EventEntity>();
    }

}
