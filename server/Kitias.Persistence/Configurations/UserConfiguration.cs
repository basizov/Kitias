using Kitias.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kitias.Persistence.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.Property(g => g.Name)
				.IsRequired();
			builder.Property(g => g.Surname)
				.IsRequired();
			builder.Property(g => g.Patronymic)
				.HasDefaultValue("");
			builder.Property(g => g.FullName)
				.HasComputedColumnSql(@"trim(""Surname"" || ' ' || ""Name"" || ' ' || ""Patronymic"")", stored: true);
		}
	}
}
