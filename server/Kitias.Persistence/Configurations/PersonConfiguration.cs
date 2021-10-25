using Kitias.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kitias.Persistence.Configurations
{
	public class PersonConfiguration : IEntityTypeConfiguration<Person>
	{
		public void Configure(EntityTypeBuilder<Person> builder)
		{
			builder.Property(p => p.Name)
				.HasMaxLength(20)
				.IsRequired();
			builder.Property(p => p.Surname)
				.HasMaxLength(30)
				.IsRequired();
			builder.Property(p => p.Patronymic)
				.HasMaxLength(30)
				.HasDefaultValue("");
			builder.Property(p => p.Email)
				.HasMaxLength(50)
				.IsRequired();
			builder.HasAlternateKey(p => p.Email);
			builder.HasIndex(p => p.Email)
				.IsUnique()
				.HasFilter(@"""Email"" IS NOT NULL")
				.HasDatabaseName("PersonEmailIndex");
			builder.Property(p => p.FullName)
				.HasComputedColumnSql(@"trim(""Surname"" || ' ' || ""Name"" || ' ' || ""Patronymic"")", stored: true);
		}
	}
}
