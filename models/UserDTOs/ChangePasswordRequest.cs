using System.ComponentModel.DataAnnotations;

namespace account_service.models.UserDTOs
{
	public class ChangePasswordRequest
	{
		[Required]
		public Guid userId { get; set; }
		[Required]
		public string newPassword { get; set; } = string.Empty;
	}
}
