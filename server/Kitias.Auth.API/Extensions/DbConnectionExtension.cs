using Kitias.Persistence;
using Kitias.Repository.Implementations.Base;
using Kitias.Repository.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kitias.Auth.API.Extensions
{
	public static class DbConnectionExtension
	{
		public static IServiceCollection AddDbConnection(this IServiceCollection services, IConfiguration config)
		{
			var pgAdminConnectionString = config.GetConnectionString("PgadminConnection");

			services.AddDbContext<DataContext>(
			  options => options.UseNpgsql(pgAdminConnectionString)
			);
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			return services;
		}
	}
}
