using System.Threading.Tasks;

namespace Kitias.Repository.Interfaces.Base
{
	public interface IUnitOfWork
	{
		IUserRepository User { get; }
		IUserRoleRepository UserRole { get; }
		IRoleRepository Role { get; }

		Task<int> SaveChangesAsync();
	}
}
