using Kitias.Persistence.Contexts;
using Kitias.Persistence.Entities.People;
using Kitias.Repository.Implementations.Base;
using Kitias.Repository.Interfaces;

namespace Kitias.Repository.Implementations
{
	/// <summary>
	/// Repository to work with teacher db
	/// </summary>
	public class TeacherRepository : Repository<Teacher>, ITeacherRepository
	{
		/// <summary>
		/// Constructor to get requred services
		/// </summary>
		/// <param name="context">Context service</param>
		public TeacherRepository(DataContext context) : base(context) { }
	}
}
