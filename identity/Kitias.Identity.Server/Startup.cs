using Kitias.Identity.Server.Modles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Kitias.Identity.Server
{
	public class Startup
	{
		private readonly IConfiguration _config;

		public Startup(IConfiguration config) => _config = config;

		public void ConfigureServices(IServiceCollection services)
		{
			var pgAdminConnectionString = _config.GetConnectionString("PgadminConnection");
			var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

			services.AddDbContext<DataContext>(o =>
				o.UseNpgsql(pgAdminConnectionString, opt =>
					opt.MigrationsAssembly(migrationAssembly)
				)
			);

			services.AddIdentity<User, Role>()
				.AddEntityFrameworkStores<DataContext>()
				.AddDefaultTokenProviders();

			services.AddIdentityServer()
				.AddAspNetIdentity<User>()
				.AddConfigurationStore(o => o.ConfigureDbContext = bldr =>
					bldr.UseNpgsql(
						pgAdminConnectionString,
						o => o.MigrationsAssembly(migrationAssembly)
					))
				.AddOperationalStore(o =>
				{
					o.ConfigureDbContext = bldr =>
						bldr.UseNpgsql(
							pgAdminConnectionString,
							o => o.MigrationsAssembly(migrationAssembly)
						);
					o.EnableTokenCleanup = true;
					o.TokenCleanupInterval = 3600;
				})
				.AddDeveloperSigningCredential();
			services.AddControllers();
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseIdentityServer();
			app.UseEndpoints(endpoints => endpoints.MapControllers());
		}
	}
}
