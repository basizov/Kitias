using Kitias.Persistence.Entities.Scheduler.Attendence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kitias.Persistence.Configurations.Data
{
	/// <summary>
	/// Fluent Validtion for attendance
	/// </summary>
	public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
	{
		public void Configure(EntityTypeBuilder<Attendance> builder)
		{
			builder.Property(a => a.Attended)
				.IsRequired()
				.HasConversion<string>()
				.HasMaxLength(15);
			builder.Property(a => a.Score)
				.IsRequired();
			builder.Property(a => a.StudentName)
				.IsRequired();
		}
	}
}
