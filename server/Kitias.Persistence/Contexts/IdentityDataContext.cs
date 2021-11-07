using Kitias.Persistence.Configurations.Identity;
using Kitias.Persistence.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Kitias.Persistence.Contexts
{
	/// <summary>
	/// Context to store identity types
	/// </summary>
	public class IdentityDataContext : IdentityDbContext<
		User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
	{
		/// <summary>
		/// Constructor to get connection
		/// </summary>
		/// <param name="options">Optios to get connection info</param>
		public IdentityDataContext(DbContextOptions<IdentityDataContext> options) : base(options) { }

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
			builder.ApplyConfiguration(new UserConfiguration());
			builder.ApplyConfiguration(new RoleConfiguration());
			builder.ApplyConfiguration(new TokenConfiguration());
		}
	}
}
