using System.ComponentModel.DataAnnotations;

namespace EventApi.Data.Entities
{
    public class StatusEntity
    {
        [Key]
        public string StatusId { get; set; } = Guid.NewGuid().ToString();

        public string StatusName { get; set; } = null!;

        public virtual ICollection<EventEntity> Events { get; set; } = new List<EventEntity>();
    }

}
