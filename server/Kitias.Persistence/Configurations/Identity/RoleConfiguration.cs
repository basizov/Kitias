using Kitias.Persistence.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kitias.Persistence.Configurations.Identity
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
