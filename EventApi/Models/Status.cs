using System.ComponentModel.DataAnnotations;

namespace EventApi.Models
{
    public class Status
    {
        public string? StatusId { get; set; } 
        
        public string StatusName { get; set; } = null!;
    }
}
