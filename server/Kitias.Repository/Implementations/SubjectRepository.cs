using Kitias.Persistence;
using Kitias.Persistence.Entities;
using Kitias.Repository.Implementations.Base;
using Kitias.Repository.Interfaces;

namespace Kitias.Repository.Implementations
{
	public class SubjectRepository : Repository<Subject>, ISubjectRepository
	{
		public SubjectRepository(DataContext dbContext) : base(dbContext) { }
	}
}
