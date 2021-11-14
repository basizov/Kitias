using Kitias.Persistence.Contexts;
using Kitias.Persistence.Entities.Scheduler.Attendence;
using Kitias.Repository.Implementations.Base;
using Kitias.Repository.Interfaces;

namespace Kitias.Repository.Implementations
{
	/// <summary>
	/// Repostiry to work with sheduker attendances
	/// </summary>
	public class ShedulerAttendaceRepository : Repository<AttendanceSheduler>, IShedulerAttendaceRepostiory
	{
		/// <summary>
		/// Constuructor to get services
		/// </summary>
		/// <param name="dbContext">Data context service</param>
		public ShedulerAttendaceRepository(DataContext dbContext) : base(dbContext) { }
	}
}
