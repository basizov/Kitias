using AutoMapper;
using Kitias.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace Kitias.API.Extensions
{
	public static class AutoMapperExtension
	{
		public static IServiceCollection AddMappingProfile(this IServiceCollection services)
		{
			var mapperConfig = new MapperConfiguration(m => m.AddProfile(new MappingProfile()));
			var mapper = mapperConfig.CreateMapper();

			services.AddSingleton(mapper);
			return services;
		}
	}
}
