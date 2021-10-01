using Kitias.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kitias.API.Extensions
{
  public static class DbConnectionExtension
  {
    public static IServiceCollection AddDbConnection(
      this IServiceCollection services,
      IConfiguration config
    )
    {
      services.AddDbContext<DataContext>(
        options => options.UseNpgsql(config.GetConnectionString("PgadminConnection"))
      );
      return (services);
    }
  }
}
