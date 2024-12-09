namespace account_service.models.UserDTOs
{
	public class HttpResponseModel
	{
		public bool Success { get; set; }
		public string? Error { get; set; }
		public string? Message { get; set; }
	}
}
