using Kitias.Persistence;
using Kitias.Persistence.Models;
using Kitias.Repository.Implementations.Base;
using Kitias.Repository.Interfaces;

namespace Kitias.Repository.Implementations
{
	public class UserRepository : Repository<User>, IUserRepository
	{
		public UserRepository(DataContext context) : base(context) { }
	}
}
