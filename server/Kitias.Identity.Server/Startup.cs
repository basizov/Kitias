using Kitias.Identity.Server.Extensions;
using Kitias.Providers.Implementations;
using Kitias.Providers.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kitias.Identity.Server
{
	public class Startup
	{
		private readonly IConfiguration _config;

		public Startup(IConfiguration config) => _config = config;

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddIdentityDb(_config);
			services.AddOwnIdentityServer();
			services.AddOpenApi();
			services.AddScoped<IAuthProvider, AuthProvider>();
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kitias.Identity.Server");
				c.RoutePrefix = string.Empty;
			});
			app.UseRouting();
			app.UseIdentityServer();
			app.UseEndpoints(endpoints => endpoints.MapControllers());
		}
	}
}
