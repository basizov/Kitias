using Kitias.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Kitias.Persistence
{
	public class DataContext : IdentityDbContext<User, Role, Guid>
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options) { }
	}
}
