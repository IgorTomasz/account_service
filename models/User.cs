using System.ComponentModel.DataAnnotations;

namespace account_service.models
{
	public class User
	{
		[Key]
		public Guid userId { get; set; }
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
		public Boolean isVerified { get; set; }
		[Required]
		public Boolean isActive { get; set; }
		[Required]
		public DateTime createdAt { get; set; }
		public DateTime lastLogin {  get; set; }
		public virtual ICollection<UserSession> sessions { get; set; }

	
	}
}
