using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace account_service.models
{
	public class UserSession
	{
		[Key]
		public Guid sessionId {  get; set; }
		[Required]
		public Guid userId { get; set; }
		[Required]
		public UserSessionStatus status { get; set; }
		[Required]
		public string token { get; set; }
		[Required]
		public DateTime startTime { get; set; }
		[Required]
		public DateTime endTime { get; set; }
		public string deviceInfo { get; set; }
		public string idAddress { get; set; }
		[ForeignKey(nameof(userId))]
		public virtual User User { get; set; }
	}

	public enum UserSessionStatus
	{
		Active, LoggedOut, Expired
	}
}
