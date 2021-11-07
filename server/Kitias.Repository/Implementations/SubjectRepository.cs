using Kitias.Persistence.Contexts;
using Kitias.Persistence.Entities.Scheduler;
using Kitias.Repository.Implementations.Base;
using Kitias.Repository.Interfaces;

namespace Kitias.Repository.Implementations
{
	/// <summary>
	/// Reposittory to work with subject db and postgres
	/// </summary>
	public class SubjectRepository : Repository<Subject>, ISubjectRepository
	{
		/// <summary>
		/// Constructor to get services
		/// </summary>
		/// <param name="dbContext">Postgre context</param>
		public SubjectRepository(DataContext dbContext) : base(dbContext) { }
	}
}
