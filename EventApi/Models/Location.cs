using System.ComponentModel.DataAnnotations;

namespace EventApi.Models
{
    public class Location
    {
        [Required]
        public string LocationId { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;

    }
}
