using Kitias.Persistence.Configurations.Data;
using Kitias.Persistence.Entities.Scheduler;
using Kitias.Persistence.Entities.People;
using Microsoft.EntityFrameworkCore;
using Kitias.Persistence.Entities.Scheduler.Attendence;

namespace Kitias.Persistence.Contexts
{
	/// <summary>
	/// Default context for simple entities
	/// </summary>
	public class DataContext : DbContext
	{
		/// <summary>
		/// Working with Person
		/// </summary>
		public DbSet<Person> Persons { get; set; }
		/// <summary>
		/// Working with Groups
		/// </summary>
		public DbSet<Group> Groups { get; set; }
		/// <summary>
		/// Working with Students
		/// </summary>
		public DbSet<Student> Students { get; set; }
		/// <summary>
		/// Working with Subjects
		/// </summary>
		public DbSet<Subject> Subjects { get; set; }
		/// <summary>
		/// Working with Teachers
		/// </summary>
		public DbSet<Teacher> Teachers { get; set; }
		/// <summary>
		/// Subject groups and group subjects
		/// </summary>
		public DbSet<SubjectGroup> SubjectsGroups { get; set; }
		/// <summary>
		/// Attendances of the subject
		/// </summary>
		public DbSet<Attendance> Attendances { get; set; }
		/// <summary>
		/// Student attendences
		/// </summary>
		public DbSet<StudentAttendance> StudentAttendances { get; set; }
		/// <summary>
		/// Sheduler of attendences
		/// </summary>
		public DbSet<AttendanceSheduler> AttendanceShedulers { get; set; }

		/// <summary>
		/// Constructor to get connetion
		/// </summary>
		/// <param name="options">Infoi about connection</param>
		public DataContext(DbContextOptions<DataContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfiguration(new GroupConfiguraion());
			builder.ApplyConfiguration(new StudentConfiguration());
			builder.ApplyConfiguration(new SubjectConfiguration());
			builder.ApplyConfiguration(new TeacherConfiguration());
			builder.ApplyConfiguration(new PersonConfiguration());
			builder.ApplyConfiguration(new AttendanceConfiguration());
			builder.ApplyConfiguration(new StudentAttendaceConfiguration());
			builder.ApplyConfiguration(new ShedulerConfiguration());
			base.OnModelCreating(builder);
		}
	}
}
