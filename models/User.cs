using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace account_service.models
{
	public class User
	{
		[Key]
		public Guid UserId { get; set; }
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
		[Required]
		public Boolean IsActive { get; set; }
		[Required]
		public DateTime CreatedAt { get; set; }
		[AllowNull]
		public DateTime? LastLogin {  get; set; }
		public virtual ICollection<UserSession> sessions { get; set; }

	
	}
}
