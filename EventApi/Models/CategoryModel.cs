using System.ComponentModel.DataAnnotations;

namespace EventApi.Models
{
    public class CategoryModel
    {
        [Required]
        public string CategoryId { get; set; } = null!;
        [Required]
        public string CategoryName { get; set; } = null!;
    }
}
