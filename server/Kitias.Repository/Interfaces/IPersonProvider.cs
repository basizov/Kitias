using Kitias.Persistence.Entities.People;
using Kitias.Repository.Interfaces.Base;

namespace Kitias.Repository.Interfaces
{
	/// <summary>
	/// Repository to work with different opportunities person db
	/// </summary>
	public interface IPersonRepository : IRepository<Person> { }
}
