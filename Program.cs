
using account_service.context;
using account_service.middleware;
using account_service.repositories;
using account_service.services;
using Microsoft.EntityFrameworkCore;

namespace account_service
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers();
			builder.Services.AddDbContext<UserDatabaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
			builder.Services.AddScoped<IUserRepository, UserRepository>();
			builder.Services.AddScoped<IUserSessionRepository, UserSessionRepository>();
			builder.Services.AddScoped<IUserService, UserService>();
			builder.Services.AddScoped<IUserSessionService, UserSessionService>();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}


			app.UseMiddleware<IpFilteringMiddleware>();


			app.UseMiddleware<GatewayAuthenticationMiddleware>();
			

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}