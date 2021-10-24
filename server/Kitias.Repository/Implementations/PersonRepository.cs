using Kitias.Persistence;
using Kitias.Persistence.Models;
using Kitias.Repository.Implementations.Base;
using Kitias.Repository.Interfaces;

namespace Kitias.Repository.Implementations
{
	public class PersonRepository : Repository<Person>, IPersonRepository
	{
		public PersonRepository(DataContext context) : base(context) { }
	}
}
