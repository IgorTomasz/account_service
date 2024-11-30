using account_service.models.UserSessionDTOs;
using account_service.services;
using Microsoft.AspNetCore.Mvc;

namespace account_service.Controllers
{
	[ApiController]
	[Route("account/[controller]")]
	public class UserSessionController : ControllerBase
	{
		private readonly IUserSessionService _userSessionService;
		private readonly ILogger<UserController> _logger;

		public UserSessionController(ILogger<UserController> logger, IUserSessionService userSessionService)
		{
			_userSessionService = userSessionService;
			_logger = logger;
		}

		[HttpPost("auth/create-session")]
		public async Task<IActionResult> CreateSession(CreateSessionRequest createSessionRequest)
		{
			Guid userSessionId = await _userSessionService.CreateUserSession(createSessionRequest);

			return Created($"/auth/session/{userSessionId}",userSessionId);
		}

		[HttpGet("auth/refresh-token/{sessionIdString}")]
		public async Task<IActionResult> GetRefreshToken(string sessionIdString)
		{
			Guid sessionId = ParseGuid(sessionIdString);

			if (sessionId == Guid.Empty)
			{
				return BadRequest("Wrong session guid");
			}

			string refToken = await _userSessionService.GetUserRefToken(sessionId);

			if (refToken == null)
			{
				return NotFound($"There is no RefToken for sessionId: {sessionId}");
			}

			return Ok(refToken);
		}

		[HttpGet("profile/userInfo/{sessionIdString}")]
		public async Task<IActionResult> GetUserIdFromSession(string sessionIdString)
		{
			Guid sessionId =  ParseGuid(sessionIdString);

			if(sessionId == Guid.Empty)
			{
				return BadRequest("Wrong session guid");
			}

			Guid userId = await _userSessionService.GetUserIdFromSessionId(sessionId);

			if (userId != Guid.Empty)
			{
				return Ok(userId);
			}
			return BadRequest("Something went wrong retreving userId from sessionId");
		}

		public Guid ParseGuid(string guidString)
		{
			Guid sessionId = Guid.Empty;

			try
			{
				sessionId = Guid.Parse(guidString);
			}
			catch (FormatException e)
			{
				
			}

			return sessionId;
		}
	}
}
