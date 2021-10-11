using Kitias.Persistence;
using Kitias.Repository.Interfaces;
using Kitias.Repository.Interfaces.Base;
using System.Threading.Tasks;

namespace Kitias.Repository.Implementations.Base
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly DataContext _context;
		private IUserRepository _user;

		public UnitOfWork(DataContext context) => _context = context;

		public IUserRepository User => _user ??= new UserRepository(_context);

		public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
	}
}
