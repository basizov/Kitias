using Kitias.Persistence;
using Kitias.Persistence.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kitias.Identity.Server.Extensions
{
	/// <summary>
	/// Extension for identity db connection
	/// </summary>
	public static class IdentityDbContextExtension
	{
		/// <summary>
		/// Method which add identity db context service
		/// </summary>
		/// <param name="services">All services</param>
		/// <param name="config">App config</param>
		/// <returns>List config with new service</returns>
		public static IServiceCollection AddIdentityDb(this IServiceCollection services, IConfiguration config)
		{
			var pgAdminConnectionString = config.GetConnectionString("PgadminConnection");

			services.AddDbContext<IdentityDataContext>(
				o => o.UseNpgsql(pgAdminConnectionString)
			);
			services.AddIdentity<User, Role>()
				.AddEntityFrameworkStores<IdentityDataContext>()
				.AddDefaultTokenProviders();
			return services;
		}
	}
}
