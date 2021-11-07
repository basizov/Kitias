using Kitias.Persistence.Contexts;
using Kitias.Repository.Implementations.Base;
using Kitias.Repository.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kitias.API.Extensions
{
	/// <summary>
	/// Extension to add database connection service
	/// </summary>
	public static class DbConnectionExtension
	{
		/// <summary>
		/// Add data base connection service
		/// </summary>
		/// <param name="services">List services</param>
		/// <param name="config">App config</param>
		/// <returns>List services with new service</returns>
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
