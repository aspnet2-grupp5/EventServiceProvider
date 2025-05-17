using System.ComponentModel.DataAnnotations;

namespace EventApi.Models
{
    public class Status
    {
        [Required]
        public string StatusId { get; set; } = null!;
        [Required]
        public string StatusName { get; set; } = null!;
    }
}
