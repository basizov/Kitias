using Kitias.Persistence.Contexts;
using Kitias.Persistence.Entities.Scheduler.Attendence;
using Kitias.Repository.Implementations.Base;
using Kitias.Repository.Interfaces;

namespace Kitias.Repository.Implementations
{
	/// <summary>
	/// Repositor to work with student attendance
	/// </summary>
	public class StudentAttendanceRepository : Repository<StudentAttendance>, IStudentAttendaceRepository
	{
		/// <summary>
		/// Constructor to get services
		/// </summary>
		/// <param name="dbContext">Data context service</param>
		public StudentAttendanceRepository(DataContext dbContext) : base(dbContext) { }
	}
}
