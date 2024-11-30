using account_service.context;
using account_service.models;
using account_service.models.UserSessionDTOs;
using Microsoft.EntityFrameworkCore;

namespace account_service.services
{
	public interface IUserSessionService
	{
		public Task<Guid> CreateUserSession(CreateSessionRequest createSessionRequest);
		public Task<string> GetUserRefToken(Guid sessionId);
		public Task<Guid> GetUserIdFromSessionId(Guid sessionId);
	}
	public class UserSessionService : IUserSessionService
	{
		private readonly UserDatabaseContext _context;

		public UserSessionService(UserDatabaseContext context)
		{
			_context = context;
		}

		public async Task<Guid> CreateUserSession(CreateSessionRequest createSessionRequest)
		{
			Guid userSessionId = Guid.NewGuid();
			var res = await _context.UserSessions.AddAsync(new UserSession
			{
				SessionId = userSessionId,
				UserId = createSessionRequest.userId,
				Status = UserSessionStatus.Active,
				Reftoken = createSessionRequest.RefToken,
				StartTime = DateTime.UtcNow,
				EndTime = DateTime.UtcNow.AddMinutes(15),
				DeviceInfo = createSessionRequest.DeviceInfo,
				IpAddress = createSessionRequest.IdAddress
			});

			await _context.SaveChangesAsync();

			if (res != null)
			{
				return userSessionId;
			}
			
			return Guid.Empty;
		}

		public async Task<string> GetUserRefToken(Guid sessionId)
		{
			var userSession = await _context.UserSessions.Where(e=> e.SessionId == sessionId).FirstOrDefaultAsync();

			if (userSession != null)
			{
				return userSession.Reftoken;
			}

			return null;
		}

		public async Task<Guid> GetUserIdFromSessionId(Guid sessionId)
		{
			var userSession = await _context.UserSessions.Where(e => e.SessionId == sessionId).FirstOrDefaultAsync();

			if (userSession != null)
			{
				return userSession.UserId;
			}

			return Guid.Empty;
		}
	}
}
