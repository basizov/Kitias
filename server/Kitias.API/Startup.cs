using Kitias.API.Extensions;
using Kitias.API.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kitias.API
{
	public class Startup
	{
		private readonly IConfiguration _config;

		public Startup(IConfiguration config) => _config = config;

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors();
			services.AddControllers();
			services.AddMappingProfile();
			services.AddDbConnection(_config);
			services.AddIdentityProps();
			services.AddProviders();
			services.AddSwagger();
		}

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
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseCookiePolicy(new CookiePolicyOptions
			{
				MinimumSameSitePolicy = SameSiteMode.Strict,
				HttpOnly = HttpOnlyPolicy.Always,
				Secure = CookieSecurePolicy.Always
			});
			app.UseCors(x => x.WithOrigins("https://localhost:3000")
				.AllowCredentials()
				.AllowAnyMethod()
				.AllowAnyHeader()
			);
			app.UseEndpoints(endpoints => endpoints.MapControllers());
		}
	}
}
