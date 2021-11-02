﻿using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kitias.API.Extensions
{
	/// <summary>
	/// Extension to authenticate token
	/// </summary>
	public static class AuthorixationExtension
	{
		/// <summary>
		/// Add authrization service
		/// </summary>
		/// <param name="services">List services</param>
		/// <returns>Services with new one</returns>
		public static IServiceCollection AddOwnAuthorization(this IServiceCollection services, IConfiguration config)
		{
			var apiName = config.GetSection("IdentityServerSecure:ApiName");
			var authority = config.GetSection("IdentityServerSecure:Authority");
			var bearer = IdentityServerAuthenticationDefaults.AuthenticationScheme;

			services.AddAuthentication(bearer)
				.AddIdentityServerAuthentication(bearer, options =>
				{
					options.ApiName = apiName.Value;
					options.Authority = authority.Value;
				});
			return services;
		}
	}
}