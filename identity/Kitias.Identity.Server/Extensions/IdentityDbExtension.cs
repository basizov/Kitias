using Kitias.Identity.Server.Models.Entities;
using Kitias.Identity.Server.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Kitias.Identity.Server.Extensions
{
	public static class IdentityDbExtension
	{
		public static IServiceCollection AddIdentityDb(
			this IServiceCollection services,
			string connectionString,
			string migrationAssembly
		)
		{
			services.AddDbContext<DataContext>(o =>
				o.UseNpgsql(connectionString, opt =>
					opt.MigrationsAssembly(migrationAssembly)
				)
			);
			services.AddIdentity<User, Role>()
				.AddEntityFrameworkStores<DataContext>()
				.AddDefaultTokenProviders();
			return services;
		}
	}
}
