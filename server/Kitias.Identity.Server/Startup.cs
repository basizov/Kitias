using Kitias.Identity.Server.Extensions;
using Kitias.Providers.Implementations;
using Kitias.Providers.Interfaces;
using Kitias.Providers.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kitias.Identity.Server
{
	/// <summary>
	/// Startup class for run the app
	/// </summary>
	public class Startup
	{
		private readonly IConfiguration _config;

		/// <summary>
		/// Constructor for additing neccesary fields
		/// </summary>
		/// <param name="config">Get the app config file</param>
		public Startup(IConfiguration config) => _config = config;

		/// <summary>
		/// Configure all neccessary services
		/// </summary>
		/// <param name="services">Initial services</param>
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddIdentityDb(_config);
			services.AddOwnIdentityServer();
			services.AddControllers();
			services.AddOpenAPI();
			services.AddScoped<EmailSender>();
			services.AddScoped<IAuthProvider, AuthProvider>();
		}

		/// <summary>
		/// Configure all neccessary middlewares
		/// </summary>
		/// <param name="app">Generate app piplines</param>
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
			app.UseAuthorization();
			app.UseEndpoints(endpoints => endpoints.MapControllers());
		}
	}
}
