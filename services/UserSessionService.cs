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
		public Task UpdateSession(Guid sessionId);
		public Task<UserSession> GetSession(Guid sessionId);
		public Guid ParseGuid(string guidString);
		public Task<List<UserSession>> GetAllSessions();
		public Task Logout(Guid sessionId);
	}
	public class UserSessionService : IUserSessionService
	{
		private readonly UserDatabaseContext _context;

		public UserSessionService(UserDatabaseContext context)
		{
			_context = context;
		}

		public async Task<UserSession> GetSession(Guid sessionId)
		{
			var userSession = await _context.UserSessions.FindAsync(sessionId);

			if (userSession == null)
			{
				return new UserSession();
			}

			return userSession;
		}

		public async Task<Guid> CreateUserSession(CreateSessionRequest createSessionRequest)
		{
			Guid userSessionId = Guid.NewGuid();
			User user = await _context.Users.FindAsync(createSessionRequest.UserId);

			var res = await _context.UserSessions.AddAsync(new UserSession
			{
				SessionId = userSessionId,
				UserId = createSessionRequest.UserId,
				Status = UserSessionStatus.Active,
				Reftoken = createSessionRequest.RefToken,
				StartTime = DateTime.UtcNow.AddHours(1),
				EndTime = DateTime.UtcNow.AddMinutes(15).AddHours(1),
				DeviceInfo = createSessionRequest.DeviceInfo,
				IpAddress = createSessionRequest.IdAddress,
				User = user
			});

			await _context.SaveChangesAsync();

			if (res != null)
			{
				return userSessionId;
			}
			
			return Guid.Empty;
		}

		public async Task Logout(Guid sessionId)
		{
			var userSession = await _context.UserSessions.Where(e=> e.SessionId== sessionId).FirstOrDefaultAsync();

			userSession.Status = UserSessionStatus.LoggedOut;
			await _context.SaveChangesAsync();
		}

		public async Task UpdateSession(Guid sessionId)
		{
			var userSession = await _context.UserSessions.Where(e=> e.SessionId == sessionId).FirstOrDefaultAsync();
			if (userSession != null)
			{
				userSession.EndTime = DateTime.UtcNow.AddMinutes(15);
			}
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

		public async Task<List<UserSession>> GetAllSessions()
		{
			return await _context.UserSessions.ToListAsync();
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
