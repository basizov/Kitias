using System.Threading.Tasks;

namespace Kitias.Repository.Interfaces.Base
{
	public interface IUnitOfWork
	{
		IPersonRepository Person { get; }
		IStudentRepository Student { get; }
		ITeacherRepository Teacher { get; }

		Task<int> SaveChangesAsync();
	}
}
