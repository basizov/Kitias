using Kitias.Identity.Server.Config;
using Kitias.Persistence.Entities.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Kitias.Identity.Server.Extensions
{
	public static class IdentityServerExtension
	{
		public static IServiceCollection AddOwnIdentityServer(this IServiceCollection services)
		{
			services.AddIdentityServer()
				.AddAspNetIdentity<User>()
				.AddInMemoryClients(OAuthOIDCConfig.TakeClients)
				.AddInMemoryApiResources(OAuthOIDCConfig.TakeAPIResources)
				.AddDeveloperSigningCredential();
			return services;
		}
	}
}
