using MedicalSystem.API.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.API
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			services.AddEndpointsApiExplorer();
			services.AddSwaggerServices();

			var connectionStrings = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection String DefaultConnection not found.");
			services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionStrings));

			return services;
		}

		private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
		{
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();

			return services;
		}
	}
}
