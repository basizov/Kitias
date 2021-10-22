using Kitias.Identity.Server.Modles;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Kitias.Identity.Server
{
	public class DataContext : IdentityDbContext<User, Role, Guid>
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options) { }
	}
}
