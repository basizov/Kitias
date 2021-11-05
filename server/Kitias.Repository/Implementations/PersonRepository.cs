using Kitias.Persistence;
using Kitias.Persistence.Entities;
using Kitias.Repository.Implementations.Base;
using Kitias.Repository.Interfaces;

namespace Kitias.Repository.Implementations
{
	/// <summary>
	/// Repository to work with person db
	/// </summary>
	public class PersonRepository : Repository<Person>, IPersonRepository
	{
		/// <summary>
		/// Constructor to get requred services
		/// </summary>
		/// <param name="context">Context service</param>
		public PersonRepository(DataContext context) : base(context) { }
	}
}
