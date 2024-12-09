using System.ComponentModel.DataAnnotations;

namespace account_service.models.UserDTOs
{
	public class RefreshTokenRequest
	{
		[Required]
		public Guid SessionId { get; set; }
		[Required]
		public Guid UserId { get; set; }
	}
}
