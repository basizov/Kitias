using Kitias.Persistence.Entities.Scheduler.Attendence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kitias.Persistence.Configurations.Data
{
	/// <summary>
	/// Fluent Validtion for studentAttendance
	/// </summary>
	public class StudentAttendaceConfiguration : IEntityTypeConfiguration<StudentAttendance>
	{
		public void Configure(EntityTypeBuilder<StudentAttendance> builder)
		{
			builder.Property(sa => sa.StudentName)
				.IsRequired();
			builder.Property(sa => sa.Grade)
				.IsRequired()
				.HasConversion<string>()
				.HasMaxLength(20);
			builder.Property(sa => sa.Raiting)
				.IsRequired();
			builder.Property(sa => sa.SubjectName)
				.IsRequired();
			builder.HasAlternateKey(sa => new { sa.StudentName, sa.SubjectName, sa.ShedulerId });
			builder.HasIndex(sa => new { sa.StudentName, sa.SubjectName, sa.ShedulerId })
				.IsUnique()
				.HasFilter(@"""StudentName"" IS NOT NULL")
				.HasDatabaseName("StudentNameIndex");
		}
	}
}
