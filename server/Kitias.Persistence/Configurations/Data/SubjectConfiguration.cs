using Kitias.Persistence.Entities.Scheduler;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kitias.Persistence.Configurations.Data
{
	/// <summary>
	/// Fluent Validtion for subject
	/// </summary>
	public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
	{
		public void Configure(EntityTypeBuilder<Subject> builder)
		{
			builder.Property(r => r.Name)
				.IsRequired()
				.HasMaxLength(50);
			builder.Property(g => g.Type)
				.IsRequired()
				.HasConversion<string>()
				.HasMaxLength(20);
			builder.Property(g => g.Time)
				.IsRequired()
				.HasColumnType("time");
			builder.Property(g => g.Date)
				.IsRequired()
				.HasColumnType("date");
			builder.Property(g => g.Week)
				.HasConversion<string>()
				.HasMaxLength(20);
			builder.Property(g => g.Day)
				.IsRequired()
				.HasConversion<string>()
				.HasMaxLength(20);
			builder.HasAlternateKey(g => new { g.Date, g.Time, g.TeacherId });
			builder.HasIndex(g => new { g.Date, g.Time, g.TeacherId })
				.IsUnique()
				.HasFilter(@"""Date"" IS NOT NULL")
				.HasDatabaseName("DateTimeIndex");
		}
	}
}
