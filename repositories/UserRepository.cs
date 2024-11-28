using account_service.context;

namespace account_service.repositories
{
	public interface IUserRepository
	{

	}

	public class UserRepository
	{
		private readonly UserDatabaseContext _context;
	}
}
