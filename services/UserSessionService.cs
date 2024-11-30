using account_service.context;
using account_service.models;
using account_service.models.UserSessionDTOs;
using account_service.repositories;
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
		private readonly IUserSessionRepository _userSessionRepository;

		public UserSessionService(IUserSessionRepository userSessionRepository)
		{
			_userSessionRepository = userSessionRepository;
		}

		public async Task<UserSession> GetSession(Guid sessionId)
		{
			return await _userSessionRepository.GetSession(sessionId);
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

		public async Task<string> GetUserRefToken(Guid sessionId)
		{
			return await _userSessionRepository.GetUserRefToken(sessionId);
		}

		public async Task<Guid> GetUserIdFromSessionId(Guid sessionId)
		{
			return await _userSessionRepository.GetUserIdFromSessionId(sessionId);
		}

		public async Task<List<UserSession>> GetAllSessions()
		{
			return await _userSessionRepository.GetAllSessions();
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
