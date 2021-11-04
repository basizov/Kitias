using Kitias.Persistence.Configurations;
using Kitias.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kitias.Persistence
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
			base.OnModelCreating(builder);
		}
	}
}
