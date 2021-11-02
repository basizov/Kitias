using Kitias.Identity.Server.Config;
using Kitias.Persistence.Entities.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Kitias.Identity.Server.Extensions
{
	/// <summary>
	/// Extension for adding identity server
	/// </summary>
	public static class IdentityServerExtension
	{
		/// <summary>
		/// Method which add identity server service
		/// </summary>
		/// <param name="services">All services</param>
		/// <returns>List config with new service</returns>
		public static IServiceCollection AddOwnIdentityServer(this IServiceCollection services)
		{
			services.AddIdentityServer()
				.AddAspNetIdentity<User>()
				.AddInMemoryClients(OAuthOIDCConfig.TakeClients)
				.AddInMemoryApiScopes(OAuthOIDCConfig.TakeScopes)
				.AddInMemoryApiResources(OAuthOIDCConfig.TakeAPIResources)
				.AddDeveloperSigningCredential();
			return services;
		}
	}
}
