using Kitias.Persistence.Configurations;
using Kitias.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kitias.Persistence
{
	public class DataContext : DbContext
	{
		public DbSet<Person> Persons { get; set; }
		public DbSet<Group> Groups { get; set; }
		public DbSet<Student> Students { get; set; }
		public DbSet<Subject> Subjects { get; set; }
		public DbSet<Teacher> Teachers { get; set; }

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
