using AutoMapper;
using Kitias.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace Kitias.API.Extensions
{
	/// <summary>
	/// Extension to add automapper service
	/// </summary>
	public static class AutoMapperExtension
	{
		/// <summary>
		/// Add automapper service
		/// </summary>
		/// <param name="services">List services</param>
		/// <returns>List services with new service</returns>
		public static IServiceCollection AddMappingProfile(this IServiceCollection services)
		{
			var mapperConfig = new MapperConfiguration(m => m.AddProfile(new MappingProfile()));
			var mapper = mapperConfig.CreateMapper();

			services.AddSingleton(mapper);
			return services;
		}
	}
}
