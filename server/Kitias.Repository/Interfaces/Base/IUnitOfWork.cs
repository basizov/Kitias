using System.Threading.Tasks;

namespace Kitias.Repository.Interfaces.Base
{
	/// <summary>
	/// Pattern to work with different dbs
	/// </summary>
	public interface IUnitOfWork
	{
		/// <summary>
		/// Person db
		/// </summary>
		IPersonRepository Person { get; }
		/// <summary>
		/// Student db
		/// </summary>
		IStudentRepository Student { get; }
		/// <summary>
		/// Techer db
		/// </summary>
		ITeacherRepository Teacher { get; }
		/// <summary>
		/// Group db
		/// </summary>
		IGroupRepository Group { get; }
		/// <summary>
		/// Subject db
		/// </summary>
		ISubjectRepository Subject { get; }
		/// <summary>
		/// Subject groups and group subjects
		/// </summary>
		ISubjectsGroupsRepository SubjectGroup { get; }
		/// <summary>
		/// Subject groups and group subjects
		/// </summary>
		IAttendanceRepository Attendance { get; }
		/// <summary>
		/// Subject groups and group subjects
		/// </summary>
		IShedulerAttendaceRepostiory ShedulerAttendace{ get; }
		/// <summary>
		/// Subject groups and group subjects
		/// </summary>
		IStudentAttendaceRepository StudentAttendace { get; }

		/// <summary>
		/// Save changes in one transaction
		/// </summary>
		/// <returns>Success status</returns>
		Task<int> SaveChangesAsync();
	}
}
