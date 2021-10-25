using Kitias.API.Extensions;
using Kitias.API.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

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
			services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Kitias.API", Version = "v1" }));
			services.AddMappingProfile();
			services.AddDbConnection(_config);
			services.AddIdentityProps();
			services.AddSwagger();
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kitias.API v1"));
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
