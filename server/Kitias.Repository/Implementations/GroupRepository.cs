using Kitias.Persistence;
using Kitias.Persistence.Entities;
using Kitias.Repository.Implementations.Base;
using Kitias.Repository.Interfaces;

namespace Kitias.Repository.Implementations
{
	public class GroupRepository : Repository<Group>, IGroupRepository
	{
		public GroupRepository(DataContext dbContext) : base(dbContext) { }
	}
}
