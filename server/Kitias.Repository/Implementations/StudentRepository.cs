using Kitias.Persistence;
using Kitias.Persistence.Entities;
using Kitias.Repository.Implementations.Base;
using Kitias.Repository.Interfaces;

namespace Kitias.Repository.Implementations
{
	public class StudentRepository : Repository<Student>, IStudentRepository
	{
		public StudentRepository(DataContext context) : base(context) { }
	}
}
