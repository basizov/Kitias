using IdentityServer4.EntityFramework.DbContexts;
using Kitias.Identity.Server.Models.Entities;
using Kitias.Identity.Server.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Kitias.Identity.Server
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();
			using var scope = host.Services.CreateScope();
			var services = scope.ServiceProvider;

			try
			{
				var dataContext = services
					.GetRequiredService<DataContext>();
				var configurationContext = services
					.GetRequiredService<ConfigurationDbContext>();
				var persistedGrantDbContext = services
					.GetRequiredService<ConfigurationDbContext>();
				var userManager = services.GetRequiredService<UserManager<User>>();

				await dataContext.Database.MigrateAsync();
				await configurationContext.Database.MigrateAsync();
				await persistedGrantDbContext.Database.MigrateAsync();
				await SeedData.SeedDataAsync(
					userManager,
					configurationContext,
					dataContext
				);
			}
			catch (Exception ex)
			{
				var logger = services.GetRequiredService<ILogger<Program>>();

				logger.LogError(ex, "Error occured during migration");
			}
			await host.RunAsync();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
	}
}
