using System.ComponentModel.DataAnnotations;

namespace EventApi.Data.Entities
{
    public class MemberEntity
    {
        [Key]
        public string MemberId { get; set; } = Guid.NewGuid().ToString();

        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;

    }

}
