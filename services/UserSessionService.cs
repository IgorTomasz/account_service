using account_service.context;
using account_service.models;
using account_service.models.UserDTOs;
using account_service.models.UserSessionDTOs;
using account_service.repositories;
using Microsoft.EntityFrameworkCore;

namespace account_service.services
{
	public interface IUserSessionService
	{
		public Task<Guid> CreateUserSession(CreateSessionRequest createSessionRequest);
		public Task<string> GetUserRefToken(RefreshTokenRequest request);
		public Task<Guid> GetUserIdFromSessionId(Guid sessionId);
		public Task UpdateSession(Guid sessionId);
		public Task<UserSession> GetSession(Guid sessionId);
		public Task<List<UserSession>> GetAllSessions();
		public Task Logout(Guid sessionId);
		public Task ExpireUserOldSessions(Guid userId);
		public Task UpdateSession(Guid sessionId, string refToken);
	}
	public class UserSessionService : IUserSessionService
	{
		private readonly IUserSessionRepository _userSessionRepository;

		public UserSessionService(IUserSessionRepository userSessionRepository)
		{
			_userSessionRepository = userSessionRepository;
		}

		public async Task<UserSession> GetSession(Guid sessionId)
		{
			return await _userSessionRepository.GetSession(sessionId);
		}

		public async Task UpdateSession(Guid sessionId, string refToken)
		{
			await _userSessionRepository.UpdateSession(sessionId, refToken);
		}

		public async Task<Guid> CreateUserSession(CreateSessionRequest createSessionRequest)
		{
			return await _userSessionRepository.CreateUserSession(createSessionRequest);
		}

		public async Task Logout(Guid sessionId)
		{
			await _userSessionRepository.Logout(sessionId);
		}

		public async Task UpdateSession(Guid sessionId)
		{
			await _userSessionRepository.UpdateSession(sessionId);
		}

		public async Task<string> GetUserRefToken(RefreshTokenRequest request)
		{
			return await _userSessionRepository.GetUserRefToken(request);
		}

		public async Task<Guid> GetUserIdFromSessionId(Guid sessionId)
		{
			return await _userSessionRepository.GetUserIdFromSessionId(sessionId);
		}

		public async Task<List<UserSession>> GetAllSessions()
		{
			return await _userSessionRepository.GetAllSessions();
		}

		public async Task ExpireUserOldSessions(Guid userId)
		{
			await _userSessionRepository.ExpireUserOldSessions(userId);
		}
	}
}
