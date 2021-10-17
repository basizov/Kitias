using Kitias.Auth.API.Interfaces;
using Kitias.Auth.API.Services;
using Kitias.Persistence;
using Kitias.Persistence.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Kitias.Auth.API.Extensions
{
	public static class IdentityExtemsion
	{
		public static IServiceCollection AddIdentityProps(this IServiceCollection services)
		{
			services.AddIdentityCore<User>(o =>
			{
				o.Password.RequireNonAlphanumeric = false;
				o.SignIn.RequireConfirmedEmail = false;
			}).AddEntityFrameworkStores<DataContext>();
			services.AddScoped<ITokensService, TokensService>();
			services.AddScoped<IUserService, UserService>();
			return services;
		}
	}
}
