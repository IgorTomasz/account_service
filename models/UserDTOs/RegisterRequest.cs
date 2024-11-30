using System.ComponentModel.DataAnnotations;

namespace account_service.models.DTOs
{
    public class RegisterRequest
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;
		[Required]
        public string Lastname { get; set; } = string.Empty;
		[Required]
        public string PasswordHash { get; set; } = string.Empty;
		[Required]
        public string Email { get; set; } = string.Empty;
		[Required]
        public DateOnly DateOfBirth { get; set; }
    }
}
