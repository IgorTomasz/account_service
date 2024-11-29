using account_service.context;
using Microsoft.EntityFrameworkCore;

namespace account_service.repositories
{
	public interface IUserRepository
	{
		public Task<Guid> IsUserExists(string username);

    }

	public class UserRepository : IUserRepository
	{
		private readonly UserDatabaseContext _context;

		public UserRepository(UserDatabaseContext context)
		{
			_context = context;
		}

		public async Task<Guid> IsUserExists(string username)
		{
			var user = await _context.Users.Where(e=> e.username==username || e.email==username).FirstOrDefaultAsync();
			if (user != null)
			{
				return user.userId;
			}
			return Guid.Empty;
		}
	}
}
