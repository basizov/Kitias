using Kitias.Providers.Implementations;
using Kitias.Providers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Kitias.API.Extensions
{
	public static class ProvidersExtension
	{
		public static IServiceCollection AddProviders(this IServiceCollection services)
		{
			services.AddScoped<IGroupProvider, GroupProvider>();
			return services;
		}
	}
}
