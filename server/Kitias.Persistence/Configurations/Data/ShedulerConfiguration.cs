using Kitias.Persistence.Entities.Scheduler.Attendence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kitias.Persistence.Configurations.Data
{
	/// <summary>
	/// Fluent Validtion for attendance sheduler
	/// </summary>
	public class ShedulerConfiguration : IEntityTypeConfiguration<AttendanceSheduler>
	{
		public void Configure(EntityTypeBuilder<AttendanceSheduler> builder)
		{
			builder.Property(a => a.SubjectName)
				.IsRequired();
			builder.Property(a => a.Name)
				.IsRequired();
			builder.HasMany(s => s.Attendances)
				.WithOne(a => a.Sheduler)
				.OnDelete(DeleteBehavior.Cascade);
			builder.HasMany(s => s.StudentAttendances)
				.WithOne(a => a.Sheduler)
				.OnDelete(DeleteBehavior.Cascade);
			builder.HasAlternateKey(a => new { a.Name, a.TeacherId });
			builder.HasIndex(a => new { a.Name, a.TeacherId })
				.IsUnique()
				.HasFilter(@"""Name"" IS NOT NULL")
				.HasDatabaseName("ShedulerNameIndex");
		}
	}
}
