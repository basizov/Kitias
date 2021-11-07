using Kitias.Persistence.Contexts;
using Kitias.Persistence.Entities.Scheduler;
using Kitias.Repository.Implementations.Base;
using Kitias.Repository.Interfaces;

namespace Kitias.Repository.Implementations
{
	/// <summary>
	/// Repository to work with group db
	/// </summary>
	public class GroupRepository : Repository<Group>, IGroupRepository
	{
		/// <summary>
		/// Constructor to get requred services
		/// </summary>
		/// <param name="context">Context service</param>
		public GroupRepository(DataContext dbContext) : base(dbContext) { }
	}
}
