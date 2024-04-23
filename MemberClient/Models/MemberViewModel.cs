using System.ComponentModel.DataAnnotations;

namespace MemberClient.Models
{
    public class MemberViewModel
    {
        public int MemberId { get; set; }
        [Required]
        [MaxLength(50)]
        public string? FullName { get; set; }
        [Required]
        [MaxLength(50)]
        [EmailAddress(ErrorMessage = "The 'Email' field must be a valid email address format.")]
        public string? Email { get; set; }
        [Required]
        [MaxLength(10)]
        public string? Gender { get; set; }
    }
}
