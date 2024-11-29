using System.ComponentModel.DataAnnotations;

namespace account_service.models.DTOs
{
    public class UserDTO
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string lastname { get; set; }
        [Required]
        public string passwordHash { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public DateOnly dateOfBirth { get; set; }
        [Required]
        public bool isVerified { get; set; }
        [Required]
        public bool isActive { get; set; }
        [Required]
        public DateTime createdAt { get; set; }
    }
}
