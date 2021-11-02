using Kitias.Persistence;
using Kitias.Persistence.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kitias.Identity.Server.Extensions
{
	public static class IdentityDbContextExtension
	{
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
