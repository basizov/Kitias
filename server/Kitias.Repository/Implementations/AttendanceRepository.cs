using Kitias.Persistence.Contexts;
using Kitias.Persistence.Entities.Scheduler.Attendence;
using Kitias.Repository.Implementations.Base;
using Kitias.Repository.Interfaces;

namespace Kitias.Repository.Implementations
{
	/// <summary>
	/// Provider to work with datacontext and attendance
	/// </summary>
	public class AttendanceRepository : Repository<Attendance>, IAttendanceRepository
	{
		/// <summary>
		/// Constructor to get services
		/// </summary>
		/// <param name="dbContext">Db context service</param>
		public AttendanceRepository(DataContext dbContext) : base(dbContext) { }
	}
}
