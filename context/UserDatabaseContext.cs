using account_service.models;
using Microsoft.EntityFrameworkCore;

namespace account_service.context
{
	public class UserDatabaseContext : DbContext
	{
		public UserDatabaseContext(DbContextOptions options) : base(options) { }

		public DbSet<User> Users { get; set; }
		public DbSet<UserSession> UserSessions { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			base.OnModelCreating(modelBuilder);
		}
	}
}
