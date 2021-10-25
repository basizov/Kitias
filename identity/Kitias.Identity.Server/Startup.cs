using Kitias.Identity.Server.Extensions;
using Microsoft.AspNetCore.Builder;
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

			services.AddIdentityDb(pgAdminConnectionString, migrationAssembly);
			services.AddOwnIdentityServer(pgAdminConnectionString, migrationAssembly);
			services.AddControllers();
			services.AddOwnAuthorization();
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseDeveloperExceptionPage();
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseIdentityServer();
			app.UseAuthorization();
			app.UseEndpoints(endpoints => endpoints.MapControllers());
		}
	}
}
