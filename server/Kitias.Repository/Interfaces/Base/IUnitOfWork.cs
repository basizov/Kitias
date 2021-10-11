using System.Threading.Tasks;

namespace Kitias.Repository.Interfaces.Base
{
	public interface IUnitOfWork
	{
		IUserRepository User { get; }

		Task<int> SaveChangesAsync();
	}
}
