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

		[HttpGet("auth/session/{sessionId}")]
		public async Task<IActionResult> GetSession(Guid sessionId)
		{

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

		[HttpGet("auth/refresh-token/{sessionId}")]
		public async Task<IActionResult> GetRefreshToken(Guid sessionId)
		{
			string refToken = await _userSessionService.GetUserRefToken(sessionId);

			if (refToken == null)
			{
				return NotFound($"There is no RefToken for sessionId: {sessionId}");
			}

			return Ok(refToken);
		}

		[HttpGet("profile/userInfo/{sessionId}")]
		public async Task<IActionResult> GetUserIdFromSession(Guid sessionId)
		{
			Guid userId = await _userSessionService.GetUserIdFromSessionId(sessionId);

			if (userId != Guid.Empty)
			{
				return Ok(new
				{
					Success = true,
					UserId = userId
				});
			}
			return BadRequest(new
			{
				Success = false,
				UserId = Guid.Empty
			});
		}

		[HttpPatch("auth/session/logout/{sessionId}")]
		public async Task<IActionResult> LogoutSession(Guid sessionId)
		{
			await _userSessionService.Logout(sessionId);

			return Ok();
		}

		

		[HttpPatch("auth/session/update/{sessionId}")]
		public async Task<IActionResult> UpdateSession(Guid sessionId)
		{
			await _userSessionService.UpdateSession(sessionId);

			return Ok($"Session {sessionId} updated");
		}



		[HttpPost("auth/session/create")]
		public async Task<IActionResult> CreateSession(CreateSessionRequest createSessionRequest)
		{
			Guid userSessionId = await _userSessionService.CreateUserSession(createSessionRequest);

			return Created($"/auth/session/{userSessionId}",userSessionId);
		}

		

		
	}
}
