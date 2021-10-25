using Kitias.Identity.Server.Configuration.FluentValidation;
using Kitias.Identity.Server.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Kitias.Identity.Server.Persistence
{
	public class DataContext : IdentityDbContext<
		User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken
	>
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<UserClaim>()
				.ToTable("UserClaims");
			builder.Entity<UserRole>()
				.ToTable("UserRoles");
			builder.Entity<UserLogin>()
				.ToTable("UserLogins");
			builder.Entity<RoleClaim>()
				.ToTable("RoleClaims");
			builder.Entity<UserToken>()
				.ToTable("UserTokens");
			builder.ApplyConfiguration(new UserConfiguration());
			builder.ApplyConfiguration(new RoleConfiguration());
			builder.ApplyConfiguration(new TokenConfiguration());
		}
	}
}
