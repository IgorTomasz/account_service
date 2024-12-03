namespace account_service.models.UserDTOs
{
	public class LoginResponse
	{
		public bool Success { get; set; }
		public string Error { get; set; } = string.Empty;
		public Guid UserId { get; set; }
	}
}
