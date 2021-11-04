using Kitias.Persistence.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kitias.Persistence.Configurations.Identity
{
	/// <summary>
	/// Fluent Validtion for user
	/// </summary>
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("Users");
			builder.HasMany(u => u.Claims)
				.WithOne(c => c.User)
				.HasForeignKey(c => c.UserId)
				.IsRequired();
			builder.HasMany(u => u.Logins)
				.WithOne(l => l.User)
				.HasForeignKey(l => l.UserId)
				.IsRequired();
			builder.HasMany(u => u.Tokens)
				.WithOne(t => t.User)
				.HasForeignKey(t => t.UserId)
				.IsRequired();
			builder.HasMany(u => u.UserRoles)
				.WithOne(ur => ur.User)
				.HasForeignKey(ur => ur.UserId)
				.IsRequired();
		}
	}
}
