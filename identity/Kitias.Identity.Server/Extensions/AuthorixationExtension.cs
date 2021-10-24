using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Kitias.Identity.Server.Extensions
{
	public static class AuthorixationExtension
	{
		public static IServiceCollection AddOwnAuthorization(this IServiceCollection services)
		{
			var bearer = IdentityServerAuthenticationDefaults.AuthenticationScheme;

			services.AddAuthentication(bearer)
				.AddIdentityServerAuthentication(bearer, options =>
				{
					options.ApiName = "kitiasApi";
					options.Authority = "https://localhost:44389/";
				});
			return services;
		}
	}
}
