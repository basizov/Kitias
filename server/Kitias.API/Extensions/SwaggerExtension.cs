using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Kitias.API.Extensions
{
	public static class SwaggerExtension
	{
		public static IServiceCollection AddSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(o =>
			{
				o.SwaggerDoc("v1", new()
				{
					Title = "Kitias",
					Version = "v1",
					Description = "Единая информационная система для студентов отделений среднего профессионального образования КИТ.",
					Contact = new()
					{
						Name = "Sizov Boris",
						Email = "boris.sizov.2001@mail.ru",
						Url = new(@"https://github.com/borissizov")
					}
				});

				o.AddSecurityDefinition("Bearer", new()
				{
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description = "JWT Authorization header using the Bearer scheme."
				});

				o.AddSecurityRequirement(new()
				{
					{
						new()
						{
							Reference = new()
							{
								Id = "Bearer",
								Type = ReferenceType.SecurityScheme
							}
						},
						Array.Empty<string>()
					}
				});

				o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
			});
			return services;
		}
	}
}
