using Kitias.Persistence;
using Kitias.Persistence.Entities;
using Kitias.Repository.Implementations.Base;
using Kitias.Repository.Interfaces;

namespace Kitias.Repository.Implementations
{
	/// <summary>
	/// Repository to work withGsubjcetGroup db
	/// </summary>
	public class SubjectsGroupsRepository : Repository<SubjectGroup>, ISubjectsGroupsRepository
	{
		/// <summary>
		/// Constructor to get requred services
		/// </summary>
		/// <param name="dbContext">Context service</param>
		public SubjectsGroupsRepository(DataContext dbContext) : base(dbContext) { }
	}
}
