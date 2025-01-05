using System.ComponentModel.DataAnnotations;

namespace account_service.models.UserSessionDTOs
{
	public class UpdateSessionWithRefToken
	{
		[Required]
		public Guid SessionId { get; set; }
		[Required]
		public string RefToken { get; set; }
	}
}
