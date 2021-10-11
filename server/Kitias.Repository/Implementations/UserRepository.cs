using Kitias.Domain.Models;
using Kitias.Persistence;
using Kitias.Repository.Implementations.Base;
using Kitias.Repository.Interfaces;

namespace Kitias.Repository.Implementations
{
	public class UserRepository : Repository<User>, IUserRepository
	{
		public UserRepository(DataContext context) : base(context) { }
	}
}
