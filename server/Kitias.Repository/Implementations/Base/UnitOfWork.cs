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
		private IUserRoleRepository _userRole;
		private IRoleRepository _role;

		public UnitOfWork(DataContext context) => _context = context;

		public IUserRepository User => _user ??= new UserRepository(_context);
		public IUserRoleRepository UserRole => _userRole ??= new UserRoleRepository(_context);
		public IRoleRepository Role => _role ??= new RoleRepository(_context);

		public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
	}
}
