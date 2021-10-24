using Kitias.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kitias.Persistence.Configurations
{
	public class PersonConfiguration : IEntityTypeConfiguration<Person>
	{
		public void Configure(EntityTypeBuilder<Person> builder)
		{
			builder.Property(g => g.Name)
				.HasMaxLength(20)
				.IsRequired();
			builder.Property(g => g.Surname)
				.HasMaxLength(30)
				.IsRequired();
			builder.Property(g => g.Patronymic)
				.HasMaxLength(30)
				.HasDefaultValue("");
			builder.Property(g => g.FullName)
				.HasComputedColumnSql(@"trim(""Surname"" || ' ' || ""Name"" || ' ' || ""Patronymic"")", stored: true);
		}
	}
}
