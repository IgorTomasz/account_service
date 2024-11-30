using System.ComponentModel.DataAnnotations;

namespace account_service.models.UserSessionDTOs
{
	public class CreateSessionRequest
	{
		[Required]
		public Guid userId {  get; set; }
		public string DeviceInfo { get; set; } = string.Empty;
		public string IdAddress { get; set; } = string.Empty;
		[Required]
		public string RefToken { get; set; } = string.Empty;

	}
}
