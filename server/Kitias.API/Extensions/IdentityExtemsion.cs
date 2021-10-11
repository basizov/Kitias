using Kitias.Persistence;
using Kitias.Persistence.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Kitias.API.Extensions
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
			return services;
		}
	}
}
