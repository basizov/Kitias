using Kitias.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kitias.Persistence.Configurations
{
	public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
	{
		public void Configure(EntityTypeBuilder<RefreshToken> builder)
		{
			builder.Property(r => r.Token)
				.IsRequired()
				.HasMaxLength(16);
			builder.Property(r => r.FingerPrint)
				.IsRequired();
			builder.Property(r => r.Ip)
				.IsRequired();
			builder.Property(r => r.Expires)
				.HasColumnType("date");
			builder.HasAlternateKey(g => g.Token);
			builder.HasIndex(g => g.Token)
				.IsUnique()
				.HasFilter("[Token] IS NOT NULL")
				.HasDatabaseName("TokenIndex");
		}
	}
}
