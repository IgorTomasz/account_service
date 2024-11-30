using account_service.models;
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

		[HttpGet("auth/session/all")]
		public async Task<IActionResult> GetAllSessions()
		{
			return Ok(await _userSessionService.GetAllSessions());
		}

		[HttpPatch("auth/session/logout/{sessionIdString}")]
		public async Task<IActionResult> LogoutSession(string sessionIdString)
		{
			Guid sessionId = _userSessionService.ParseGuid(sessionIdString);
			if (sessionId == Guid.Empty)
			{
				return BadRequest("Wrong session guid");
			}

			await _userSessionService.Logout(sessionId);

			return Ok();
		}

		[HttpGet("auth/session/{sessionIdString}")]
		public async Task<IActionResult> GetSession(string sessionIdString)
		{
			Guid sessionId = _userSessionService.ParseGuid(sessionIdString);

			if (sessionId == Guid.Empty)
			{
				return BadRequest("Wrong session guid");
			}

			UserSession userSession = await _userSessionService.GetSession(sessionId);

			if (userSession == null)
			{
				return NotFound("Session was not found");
			}

			return Ok(new
			{
				SessionId = userSession.SessionId,
				UserId = userSession.UserId,
				Status = userSession.Status,
				Reftoken = userSession.Reftoken,
				StartTime = userSession.StartTime,
				EndTime = userSession.EndTime,
				DeviceInfo = userSession.DeviceInfo,
				IpAddress = userSession.IpAddress
			});
		}

		[HttpPatch("auth/update-session/{sessionIdString}")]
		public async Task<IActionResult> UpdateSession(string sessionIdString)
		{
			Guid sessionId = _userSessionService.ParseGuid(sessionIdString);

			if (sessionId == Guid.Empty)
			{
				return BadRequest("Wrong session guid");
			}

			await _userSessionService.UpdateSession(sessionId);

			return Ok($"Session {sessionId} updated");
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
			Guid sessionId = _userSessionService.ParseGuid(sessionIdString);

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
			Guid sessionId = _userSessionService.ParseGuid(sessionIdString);

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

		
	}
}
