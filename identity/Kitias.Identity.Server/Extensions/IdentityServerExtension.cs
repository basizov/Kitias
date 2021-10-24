using IdentityServer4.AccessTokenValidation;
using Kitias.Identity.Server.Models.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Kitias.Identity.Server.Extensions
{
	public static class IdentityServerExtension
	{
		public static IServiceCollection AddOwnIdentityServer(
			this IServiceCollection services,
			string connectionString,
			string migrationAssembly
		)
		{
			services.AddIdentityServer()
				.AddAspNetIdentity<User>()
				.AddConfigurationStore(o => o.ConfigureDbContext = bldr =>
					bldr.UseNpgsql(
						connectionString,
						o => o.MigrationsAssembly(migrationAssembly)
					))
				.AddOperationalStore(o =>
				{
					o.ConfigureDbContext = bldr =>
						bldr.UseNpgsql(
							connectionString,
							o => o.MigrationsAssembly(migrationAssembly)
						);
					o.EnableTokenCleanup = true;
					o.TokenCleanupInterval = 3600;
				})
				.AddDeveloperSigningCredential();
			return services;
		}
	}
}
