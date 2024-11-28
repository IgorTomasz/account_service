
namespace account_service.middleware
{
	public class GatewayAuthentication
	{
		private readonly RequestDelegate _requestDelegate;
		private readonly string _secret;

		public GatewayAuthentication(IConfiguration configuration, RequestDelegate requestDelegate)
		{
			_secret = configuration["secret"];
			_requestDelegate = requestDelegate;
		}

		public async Task Invoke(HttpContext context)
		{
			var header = context.Request.Headers["X-Int-Secret"];
			if (header != _secret)
			{
				context.Response.StatusCode = 403;
				await context.Response.WriteAsync("Unauthorized");
				return;
			}

			await _requestDelegate(context);
		}
	}
}
