using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Kitias.API.Extensions
{
	public static class IdentityExtemsion
	{
		public static IServiceCollection AddIdentityProps(this IServiceCollection services)
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
