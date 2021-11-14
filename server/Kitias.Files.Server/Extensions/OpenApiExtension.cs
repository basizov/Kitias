using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;

namespace Kitias.Files.Server.Extensions
{
	/// <summary>
	/// Extension for adding open api service
	/// </summary>
	public static class OpenApiExtension
	{
		/// <summary>
		/// Method which add OpenApi service
		/// </summary>
		/// <param name="services">All services</param>
		/// <returns>List config with new service</returns>
		public static IServiceCollection AddOpenAPI(this IServiceCollection services)
		{
			services.AddSwaggerGen(o =>
			{
				o.SwaggerDoc("v1", new()
				{
					Title = "Kitias.Files.Server",
					Version = "v1",
					Description = "Web API for protecting deffenent Kitias APIs",
					Contact = new()
					{
						Name = "Sizov Boris",
						Email = "boris.sizov.2001@mail.ru",
						Url = new(@"https://github.com/borissizov")
					}
				});
				o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
			});
			return services;
		}
	}
}
