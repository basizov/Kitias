using System;
using Kitias.Persistence.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kitias.Persistence
{
  public class DataContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
  {
    public DataContext(DbContextOptions<DataContext> options): base(options) { }
  }
}
