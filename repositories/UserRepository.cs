using account_service.context;
using account_service.models;
using account_service.models.DTOs;
using account_service.models.UserDTOs;
using account_service.repositories;
using Microsoft.EntityFrameworkCore;

namespace account_service.repositories
{
	public interface IUserRepository
	{
		public Task<Guid> CheckIfUserExists(string username);
		public Task<List<User>> GetAllUsers();
		public Task<User> CreateUser(RegisterRequest userDTO);
		public Task<bool> CheckPasswordMatchUser(Guid userId, string password);
		public Task<bool> CheckIfAlreadyExistsByUsernameAndEmail(string username, string email);
		public Task<User> GetUserProfile(Guid userId);
		public Task<bool> CheckIfUserExists(Guid userId);
		public Task<bool> ChangePassword(ChangePasswordRequest passwordRequest);
		public Task DeleteUser(Guid userId);
		public Task UpdateLastLogin(Guid userId);

    }

	public class UserRepository : IUserRepository
	{
		private readonly UserDatabaseContext _context;

		public UserRepository(UserDatabaseContext context)
		{
			_context = context;
		}

		public async Task<User> GetUserProfile(Guid userId)
		{
			var user = await _context.Users.Where(e => e.UserId == userId).FirstOrDefaultAsync();

			return user != null ? user : new User();
		}

		public async Task<bool> CheckIfAlreadyExistsByUsernameAndEmail(string username, string email)
		{
			var user = await _context.Users.Where(e => e.Username == username || e.Email == email).FirstOrDefaultAsync();

			return user != null;
		}

		public async Task<Guid> CheckIfUserExists(string username)
		{
			var user = await _context.Users.Where(e => e.Username == username || e.Email == username).FirstOrDefaultAsync();

			return user != null ? user.UserId : Guid.Empty;
		}

		public async Task<bool> CheckIfUserExists(Guid userId)
		{
			var user = await _context.Users.FindAsync(userId);

			return user != null;
		}

		public async Task<bool> CheckPasswordMatchUser(Guid userId, string password)
		{


			var user = await _context.Users.Where(e => e.UserId == userId && e.PasswordHash == password).FirstOrDefaultAsync();

			return user != null;
		}

		public async Task UpdateLastLogin(Guid userId)
		{
            var user = await _context.Users.Where(e => e.UserId == userId).FirstOrDefaultAsync();

			user.LastLogin = DateTime.Now.AddHours(1);
			await _context.SaveChangesAsync();
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
				LastLogin = null,
				UserType = UserType.Client
			};

			var id = await _context.Users.AddAsync(user);

			await _context.SaveChangesAsync();

			return user;
		}

		public async Task<bool> ChangePassword(ChangePasswordRequest passwordRequest)
		{
			var user = await _context.Users.FindAsync(passwordRequest.userId);

			if (passwordRequest.newPassword == user.PasswordHash)
			{
				return false;
			}

			user.PasswordHash = passwordRequest.newPassword;

			var res = await _context.SaveChangesAsync();

			return res >= 1;

		}

		public async Task DeleteUser(Guid userId)
		{
			var user = await _context.Users.FindAsync(userId);
			_context.Users.Remove(user);
			await _context.SaveChangesAsync();
		}

	}
}
