using Kitias.Persistence;
using Kitias.Repository.Interfaces;
using Kitias.Repository.Interfaces.Base;
using System.Threading.Tasks;

namespace Kitias.Repository.Implementations.Base
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly DataContext _context;
		private IPersonRepository _person;
		private IStudentRepository _student;
		private ITeacherRepository _teacher;
		private IGroupRepository _group;
		private ISubjectRepository _subject;
		private ISubjectsGroupsRepository _subjectGroup;

		public UnitOfWork(DataContext context) => _context = context;

		public IPersonRepository Person => _person ??= new PersonRepository(_context);
		public IStudentRepository Student => _student ??= new StudentRepository(_context);
		public ITeacherRepository Teacher => _teacher ??= new TeacherRepository(_context);
		public IGroupRepository Group => _group ??= new GroupRepository(_context);
		public ISubjectRepository Subject => _subject ??= new SubjectRepository(_context);
		public ISubjectsGroupsRepository SubjectGroup => _subjectGroup ??= new SubjectsGroupsRepository(_context);

		public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
	}
}
