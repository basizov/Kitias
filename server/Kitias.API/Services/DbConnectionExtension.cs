using Kitias.Persistence;
using Kitias.Repository.Implementations.Base;
using Kitias.Repository.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kitias.API.Extensions
{
	public static class DbConnectionExtension
	{
		public static IServiceCollection AddDbConnection(this IServiceCollection services, IConfiguration config)
		{
			services.AddDbContext<DataContext>(
			  options => options.UseNpgsql(config.GetConnectionString("PgadminConnection"))
			);
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			return services;
		}
	}
}
