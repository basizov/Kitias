using Kitias.Persistence.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kitias.Persistence.Configurations.Identity
{
	/// <summary>
	/// Fluent Validtion for userToken
	/// </summary>
	public class TokenConfiguration : IEntityTypeConfiguration<UserToken>
	{
		public void Configure(EntityTypeBuilder<UserToken> builder)
		{
			builder.ToTable("UserTokens");
			builder.HasKey(ut => ut.Id);
		}
	}
}
