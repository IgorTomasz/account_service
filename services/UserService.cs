using account_service.context;
using account_service.models;
using account_service.models.DTOs;
using account_service.models.UserDTOs;
using account_service.repositories;
using Microsoft.EntityFrameworkCore;

namespace account_service.services
{
	public interface IUserService
	{
		public Task<Guid> CheckIfUserExists(string username);
		public Task<List<User>> GetAllUsers();
		public Task<User> CreateUser(RegisterRequest userDTO);
		public Task<bool> CheckPasswordMatchUser(Guid userId, string password);
		public Task<bool> CheckIfAlreadyExistsByUsernameAndEmail(string username, string email);
		public Task<User> GetUserProfile(Guid userId);
		public Guid ParseGuid(string guidString);
		public Task<bool> CheckIfUserExists(Guid userId);
		public Task<bool> ChangePassword(ChangePasswordRequest passwordRequest);

	}

	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<User> GetUserProfile(Guid userId)
		{
			return await _userRepository.GetUserProfile(userId);
		}

		public async Task<bool> CheckIfAlreadyExistsByUsernameAndEmail(string username, string email)
		{
			return await _userRepository.CheckIfAlreadyExistsByUsernameAndEmail(username, email);
		}

		public async Task<Guid> CheckIfUserExists(string username)
		{
			return await _userRepository.CheckIfUserExists(username);
		}

		public async Task<bool> CheckIfUserExists(Guid userId)
		{
			return await _userRepository.CheckIfUserExists(userId); ;
		}

		public async Task<bool> CheckPasswordMatchUser(Guid userId, string password)
		{
			return await _userRepository.CheckPasswordMatchUser(userId, password);
		}

		public async Task<List<User>> GetAllUsers()
		{
			return await _userRepository.GetAllUsers();
		} 

		public async Task<User> CreateUser(RegisterRequest userDTO)
		{
			return await _userRepository.CreateUser(userDTO);
		}

		public async Task<bool> ChangePassword(ChangePasswordRequest passwordRequest)
		{
			return await _userRepository.ChangePassword(passwordRequest);

		}

		public Guid ParseGuid(string guidString)
		{
			Guid userId = Guid.Empty;

			try
			{
				userId = Guid.Parse(guidString);
			}
			catch (FormatException e)
			{

			}

			return userId;
		}
	}
}
