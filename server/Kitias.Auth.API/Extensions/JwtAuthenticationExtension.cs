using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Kitias.Auth.API.Extensions
{
	public static class JwtAuthenticationExtension
	{
		public static IServiceCollection AddJwtAutentication(this IServiceCollection services, IConfiguration config)
		{
			var jwtAuthenticationSecret = config.GetConnectionString("JwtAuthenticationSecret");
			var jwtBytesFromAS = Encoding.UTF8.GetBytes(jwtAuthenticationSecret);

			services.AddAuthentication(o =>
			{
				o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(o =>
			{
				o.SaveToken = true;
				o.RequireHttpsMetadata = true;
				o.TokenValidationParameters = new TokenValidationParameters
				{
					IssuerSigningKey = new SymmetricSecurityKey(jwtBytesFromAS),
					ClockSkew = TimeSpan.Zero,
					ValidateIssuerSigningKey = true,
					ValidateLifetime = true,
					ValidateIssuer = false,
					ValidateAudience = false
				};
			});
			return services;
		}
	}
}
