using System.ComponentModel.DataAnnotations;

namespace EventApi.Data.Entities
{
    public class AdminEntity
    {
        [Key]
        public string AdminId { get; set; } = Guid.NewGuid().ToString();

        public string AdminName { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
