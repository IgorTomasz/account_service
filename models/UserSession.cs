using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace account_service.models
{
	public class UserSession
	{
		[Key]
		public Guid SessionId {  get; set; }
		[Required]
		public Guid UserId { get; set; }
		[Required]
		public UserSessionStatus Status { get; set; }
		[Required]
		public string Reftoken { get; set; } = string.Empty;
		[Required]
		public DateTime StartTime { get; set; }
		[Required]
		public DateTime EndTime { get; set; }
		public string DeviceInfo { get; set; } = string.Empty;
		public string IpAddress { get; set; } = string.Empty;
		[ForeignKey(nameof(UserId))]
		public virtual User User { get; set; }
	}

	public enum UserSessionStatus
	{
		Active, LoggedOut, Expired
	}
}
