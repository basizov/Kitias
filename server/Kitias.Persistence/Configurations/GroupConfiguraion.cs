using Kitias.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kitias.Persistence.Configurations
{
	public class GroupConfiguraion : IEntityTypeConfiguration<Group>
	{
		public void Configure(EntityTypeBuilder<Group> builder)
		{
			builder.Property(g => g.Course)
				.IsRequired();
			builder.Property(g => g.Number)
				.IsRequired()
				.HasMaxLength(10);
			builder.Property(g => g.EducationType)
				.IsRequired()
				.HasConversion<string>()
				.HasMaxLength(20);
			builder.Property(g => g.Speciality)
				.IsRequired()
				.HasConversion<string>()
				.HasMaxLength(50);
			builder.Property(g => g.ReceiptDate)
				.IsRequired();
			builder.Property(g => g.IssueDate)
				.IsRequired();
			builder.HasAlternateKey(g => g.Number);
			builder.HasIndex(g => g.Number)
				.IsUnique()
				.HasFilter(@"""Number"" IS NOT NULL")
				.HasDatabaseName("GroupNameIndex");
		}
	}
}
