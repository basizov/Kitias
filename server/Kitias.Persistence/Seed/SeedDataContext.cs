using Kitias.Persistence.Contexts;
using System.Threading.Tasks;

namespace Kitias.Persistence.Seed
{
	/// <summary>
	/// Seed postgres context
	/// </summary>
	public static class SeedDataContext
	{
		public static async Task SeedAttendances(DataContext context) => await Task.CompletedTask;
	}
}
