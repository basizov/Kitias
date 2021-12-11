using Kitias.API.Extensions;
using Kitias.API.Middlewares;
using Kitias.Persistence.Entities.Default;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kitias.API
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
			services.AddControllers();
			services.AddCors(o => o.AddPolicy("CorsPolicy", policy => policy
				.WithOrigins("http://localhost:3000")
				.WithExposedHeaders("Access-Control-Expose-Headers", "Content-Disposition")
				.AllowAnyHeader()
				.AllowAnyMethod()
				.AllowCredentials()
			));
			services.AddHttpClient();
			services.AddOwnAuthorization(_config);
			services.AddMappingProfile();
			services.AddDbConnection(_config);
			services.AddProviders();
			services.AddOpenAPI();
			services.Configure<ISSecure>(_config.GetSection("IdentityServerSecure"));
		}

		/// <summary>
		/// Configure all neccessary middlewares
		/// </summary>
		/// <param name="app">Generate app piplines</param>
		public void Configure(IApplicationBuilder app)
		{
			app.UseSwagger();
			app.UseSwaggerUI(o =>
			{
				o.SwaggerEndpoint("/swagger/v1/swagger.json", "Kitias.API");
				o.RoutePrefix = string.Empty;
			});
			app.UseMiddleware<AuthenticationMiddleware>();
			app.UseMiddleware<ErrorHandlerMiddleware>();
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseCors("CorsPolicy");
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseCookiePolicy(new CookiePolicyOptions
			{
				MinimumSameSitePolicy = SameSiteMode.Strict,
				HttpOnly = HttpOnlyPolicy.Always,
				Secure = CookieSecurePolicy.Always
			});
			app.UseEndpoints(endpoints => endpoints.MapControllers());
		}
	}
}
