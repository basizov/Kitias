using Kitias.Persistence.Contexts;
using Kitias.Repository.Interfaces;
using Kitias.Repository.Interfaces.Base;
using System.Threading.Tasks;

namespace Kitias.Repository.Implementations.Base
{
	/// <summary>
	/// Pattern to work with different dbs
	/// </summary>
	public class UnitOfWork : IUnitOfWork
	{
		private readonly DataContext _context;
		private IPersonRepository _person;
		private IStudentRepository _student;
		private ITeacherRepository _teacher;
		private IGroupRepository _group;
		private ISubjectRepository _subject;
		private ISubjectsGroupsRepository _subjectGroup;

		/// <summary>
		/// Consturctor to get neccesary services
		/// </summary>
		/// <param name="context">Context</param>
		public UnitOfWork(DataContext context) => _context = context;

		/// <summary>
		/// Repostiry to wotk with Person
		/// </summary>
		public IPersonRepository Person => _person ??= new PersonRepository(_context);
		/// <summary>
		/// Repostiry to wotk with Student
		/// </summary>
		public IStudentRepository Student => _student ??= new StudentRepository(_context);
		/// <summary>
		/// Repostiry to wotk with Teacher
		/// </summary>
		public ITeacherRepository Teacher => _teacher ??= new TeacherRepository(_context);
		/// <summary>
		/// Repostiry to wotk with Group
		/// </summary>
		public IGroupRepository Group => _group ??= new GroupRepository(_context);
		/// <summary>
		/// Repostiry to wotk with Subject
		/// </summary>
		public ISubjectRepository Subject => _subject ??= new SubjectRepository(_context);
		/// <summary>
		/// Repostiry to wotk with SubjectGroup
		/// </summary>
		public ISubjectsGroupsRepository SubjectGroup => _subjectGroup ??= new SubjectsGroupsRepository(_context);

		public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
	}
}
