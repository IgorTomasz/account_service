using account_service.repositories;

namespace account_service.services
{
	public interface IUserService
	{
		public Task<Guid> CheckIfUserExists(string username);

    }

	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{ 
			_userRepository = userRepository;
		}

		public async Task<Guid> CheckIfUserExists(string username)
		{
			return await _userRepository.IsUserExists(username);
		}

    }
}
