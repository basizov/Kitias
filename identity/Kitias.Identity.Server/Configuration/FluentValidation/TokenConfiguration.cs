using Kitias.Identity.Server.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kitias.Identity.Server.Configuration.FluentValidation
{
	public class TokenConfiguration : IEntityTypeConfiguration<UserToken>
	{
		public void Configure(EntityTypeBuilder<UserToken> builder)
		{
			builder.ToTable("UserTokens");
			builder.HasKey(ut => ut.Id);
			//builder.HasAlternateKey(ut => ut.Value);
			//builder.HasIndex(ut => ut.Value)
			//	.IsUnique()
			//	.HasFilter(@"""Value"" IS NOT NULL")
			//	.HasDatabaseName("RefreshTokenIndex");
		}
	}
}
