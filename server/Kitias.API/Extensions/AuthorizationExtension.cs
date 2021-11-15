using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kitias.API.Extensions
{
	/// <summary>
	/// Extension to authenticate token
	/// </summary>
	public static class AuthorizationExtension
	{
		/// <summary>
		/// Add authrization service
		/// </summary>
		/// <param name="services">List services</param>
		/// <param name="config">Config to get necessary values</param>
		/// <returns>Services with new one</returns>
		public static IServiceCollection AddOwnAuthorization(this IServiceCollection services, IConfiguration config)
		{
			var apiName = config.GetSection("IdentityServerSecure:ApiName");
			var authority = config.GetSection("IdentityServerSecure:Authority");
			var bearer = IdentityServerAuthenticationDefaults.AuthenticationScheme;

			services.AddAuthentication(bearer)
				.AddJwtBearer(bearer, o =>
				{
					o.Authority = authority.Value;
					o.Audience = apiName.Value;
				});
			return services;
		}
	}
}
