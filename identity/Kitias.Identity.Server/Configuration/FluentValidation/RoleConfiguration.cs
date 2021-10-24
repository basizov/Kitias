using Kitias.Identity.Server.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kitias.Identity.Server.Configuration.FluentValidation
{
	public class RoleConfiguration : IEntityTypeConfiguration<Role>
	{
		public void Configure(EntityTypeBuilder<Role> builder)
		{
			builder.ToTable("Roles");
			builder.HasMany(r => r.Claims)
				.WithOne(c => c.Role)
				.HasForeignKey(c => c.RoleId)
				.IsRequired();
			builder.HasMany(r => r.UserRoles)
				.WithOne(c => c.Role)
				.HasForeignKey(c => c.RoleId)
				.IsRequired();
		}
	}
}
