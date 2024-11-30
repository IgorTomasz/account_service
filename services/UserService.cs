using account_service.context;
using account_service.models;
using account_service.models.DTOs;

using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

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

	}

	public class UserService : IUserService
	{
		private readonly UserDatabaseContext _context;

		public UserService(UserDatabaseContext context)
		{
			_context = context;
		}

		public async Task<User> GetUserProfile(Guid userId)
		{
			var user = await _context.Users.Where(e => e.UserId == userId).FirstOrDefaultAsync();
			if (user != null)
			{
				return user;
			}
			return new User();
		}

		public async Task<bool> CheckIfAlreadyExistsByUsernameAndEmail(string username, string email)
		{
			var user = await _context.Users.Where(e => e.Username == username || e.Email == email).FirstOrDefaultAsync();

			if (user == null)
			{
				return false;
			}
			return true;
		}

		public async Task<Guid> CheckIfUserExists(string username)
		{
			var user = await _context.Users.Where(e => e.Username == username || e.Email == username).FirstOrDefaultAsync();
			if (user != null)
			{
				return user.UserId;
			}
			return Guid.Empty;
		}

		public async Task<bool> CheckPasswordMatchUser(Guid userId, string password)
		{


			var user = await _context.Users.Where(e => e.UserId == userId && e.PasswordHash == password).FirstOrDefaultAsync();

			if (user != null)
			{
				return true;
			}
			return false;
		}

		public async Task<List<User>> GetAllUsers()
		{
			var list = await _context.Users.ToListAsync();
			return list;
		} 

		public async Task<User> CreateUser(RegisterRequest userDTO)
		{
			var g = Guid.NewGuid();

			User user = new User
			{
				UserId = g,
				Name = userDTO.Name,
				Lastname = userDTO.Lastname,
				Email = userDTO.Email,
				Username = userDTO.Username,
				IsActive = true,
				CreatedAt = DateTime.Now,
				PasswordHash = userDTO.PasswordHash,
				DateOfBirth = userDTO.DateOfBirth,
				LastLogin = null
			};

			var id = await _context.Users.AddAsync(user);

			await _context.SaveChangesAsync();

			return user;
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
