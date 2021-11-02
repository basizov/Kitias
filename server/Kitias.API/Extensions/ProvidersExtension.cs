using Kitias.Providers.Implementations;
using Kitias.Providers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Kitias.API.Extensions
{
	/// <summary>
	/// Extension to plug providers
	/// </summary>
	public static class ProvidersExtension
	{
		/// <summary>
		/// Add providers
		/// </summary>
		/// <param name="services">List services</param>
		/// <returns>List services with new service</returns>
		public static IServiceCollection AddProviders(this IServiceCollection services)
		{
			services.AddScoped<IGroupProvider, GroupProvider>();
			return services;
		}
	}
}
