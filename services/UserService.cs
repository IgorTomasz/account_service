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
		public Task<List<User>> getAllUsers();
		public Task createUser(UserDTO userDTO);

	}

	public class UserService : IUserService
	{
		private readonly UserDatabaseContext _context;

		public UserService(UserDatabaseContext context)
		{
			_context = context;
		}

		public async Task<Guid> CheckIfUserExists(string username)
		{
			var user = await _context.Users.Where(e => e.username == username || e.email == username).FirstOrDefaultAsync();
			if (user != null)
			{
				return user.userId;
			}
			return Guid.Empty;
		}

		public async Task<List<User>> getAllUsers()
		{
			var list = await _context.Users.ToListAsync();
			return list;
		} 

		public async Task createUser(UserDTO userDTO)
		{
			byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
			var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(password: userDTO.passwordHash, salt: salt, prf: KeyDerivationPrf.HMACSHA256, iterationCount: 1000, numBytesRequested: 256 / 8));
			var g = Guid.NewGuid();

			var id = await _context.Users.AddAsync(new User
			{
				userId = g,
				name = userDTO.name,
				lastname = userDTO.lastname,
				email = userDTO.email,
				username = userDTO.username,
				isActive = userDTO.isActive,
				isVerified = userDTO.isVerified,
				createdAt = DateTime.Now,
				passwordHash = hashed,
				dateOfBirth = userDTO.dateOfBirth,
				lastLogin = null
			});

			await _context.SaveChangesAsync();
		}
    }
}
