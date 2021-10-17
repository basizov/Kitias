using Kitias.Persistence.Configurations;
using Kitias.Persistence.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Kitias.Persistence
{
	public class DataContext : IdentityDbContext<User, Role, Guid>
	{
		public DbSet<Group> Groups { get; set; }
		public DbSet<RefreshToken> RefreshTokens { get; set; }
		public DbSet<Student> Students { get; set; }
		public DbSet<Subject> Subjects { get; set; }
		public DbSet<Teacher> Teachers { get; set; }

		public DataContext(DbContextOptions<DataContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfiguration(new GroupConfiguraion());
			builder.ApplyConfiguration(new RefreshTokenConfiguration());
			builder.ApplyConfiguration(new StudentConfiguration());
			builder.ApplyConfiguration(new SubjectConfiguration());
			builder.ApplyConfiguration(new TeacherConfiguration());
			builder.ApplyConfiguration(new UserConfiguration());
			base.OnModelCreating(builder);
		}
	}
}
