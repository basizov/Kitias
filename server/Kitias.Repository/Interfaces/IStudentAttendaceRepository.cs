using Kitias.Persistence.Entities.Scheduler.Attendence;
using Kitias.Repository.Interfaces.Base;

namespace Kitias.Repository.Interfaces
{
	/// <summary>
	/// Repostiryo to work with DataContext and StudentAttendace
	/// </summary>
	public interface IStudentAttendaceRepository : IRepository<StudentAttendance> { }
}
