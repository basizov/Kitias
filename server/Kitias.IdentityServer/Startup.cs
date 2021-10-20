using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Kitias.IdentityServer
{
	public class Startup
	{ 
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddIdentityServer()
				.AddInMemoryApiResources(Configuration.Configuration.GetApiResources)
				.AddInMemoryApiScopes(Configuration.Configuration.GetApiScopes)
				.AddInMemoryIdentityResources(Configuration.Configuration.GetIdentityResources)
				.AddInMemoryClients(Configuration.Configuration.GetClients);
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
