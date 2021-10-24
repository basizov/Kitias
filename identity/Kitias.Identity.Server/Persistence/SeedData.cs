using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Kitias.Identity.Server.Models.Entities;
using Kitias.Identity.Server.OAuthOIDC.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitias.Identity.Server.Persistence
{
	public static class SeedData
	{
		public static async Task SeedDataAsync(
			UserManager<User> userManager,
			ConfigurationDbContext configurationContext,
			DataContext dataContext
		)
		{
			if (!userManager.Users.Any())
			{
				var users = new List<User>
				{
					new() { UserName = "boris", Email = "boris.sizov.2001@mail.ru" },
					new() { UserName = "adel", Email = "adel@mail.ru" },
					new() { UserName = "vova", Email = "vladimir@mail.ru" }
				};

				foreach (var user in users)
					await userManager.CreateAsync(user, "Pa$$w0rd");
			}
			if (!dataContext.Roles.Any())
			{
				var roles = new List<Role>
				{
					new() { Name = "Admin" },
					new() { Name = "Student" },
					new() { Name = "Teacher" }
				};

				roles.ForEach(async role => await dataContext.Roles.AddAsync(role));
			}
			if (!dataContext.UserRoles.Any())
			{
				await dataContext.SaveChangesAsync();
				var users = dataContext.Users.AsEnumerable();
				var adminRole = await dataContext.Roles
					.SingleOrDefaultAsync(r => r.Name == "Admin");
				var studentRole = await dataContext.Roles
					.SingleOrDefaultAsync(r => r.Name == "Student");

				foreach (var user in users)
				{
					if (user.UserName.Equals("boris"))
					{
						await dataContext.UserRoles.AddAsync(new()
						{
							UserId = user.Id,
							RoleId = adminRole.Id
						});
					}
					else
					{
						await dataContext.UserRoles.AddAsync(new()
						{
							UserId = user.Id,
							RoleId = studentRole.Id
						});
					}
				}
			}
			if (!configurationContext.Clients.Any())
			{
				Config.GetClients.ForEach(async c => await configurationContext.Clients.AddAsync(c.ToEntity()));
			}
			if (!configurationContext.IdentityResources.Any())
			{
				Config.GetIdentityResources.ForEach(async ir => await configurationContext.IdentityResources.AddAsync(ir.ToEntity()));
			}
			if (!configurationContext.ApiScopes.Any())
			{
				Config.GetApiScopes.ForEach(async asp => await configurationContext.ApiScopes.AddAsync(asp.ToEntity()));
			}
			if (!configurationContext.ApiResources.Any())
			{
				Config.GetApiResources.ForEach(async ar => await configurationContext.ApiResources.AddAsync(ar.ToEntity()));
			}
			await dataContext.SaveChangesAsync();
			await configurationContext.SaveChangesAsync();
		}
	}
}
