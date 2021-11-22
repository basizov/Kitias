using Kitias.Persistence.DTOs;
using Kitias.Providers.Models;
using Kitias.Providers.Models.Person;
using Kitias.Providers.Models.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitias.Providers.Interfaces
{
	/// <summary>
	/// Provider to work with teacher db
	/// </summary>
	public interface ITeacherProvider
	{
		/// <summary>
		/// Take all teacher from db
		/// </summary>
		/// <returns>Teacher</returns>
		Result<IEnumerable<TeacherDto>> TakeTeachers();
		/// <summary>
		/// Take only one te teacher db
		/// </summary>
		/// <param name="id">Id of the teacher to take</param>
		/// <returns>Teacher</returns>
		Task<Result<TeacherDto>> TakeTeacherByIdAsync(Guid id);
		/// <summary>
		/// Take all subjects of the teacher
		/// </summary>
		/// <param name="email">Email of the teacher to take</param>
		/// <returns>Subjects</returns>
		Task<Result<IEnumerable<SubjectDto>>> TakeTeacherSubjectsAsync(string email);
		/// <summary>
		/// Take subject of the teacher by name
		/// </summary>
		/// <param name="email">Email of the teacher to take</param>
		/// <param name="name">Subject name</param>
		/// <returns>Subject</returns>
		Task<Result<IEnumerable<SubjectDto>>> TakeTeacherSubjectAsync(string email, string name);
		/// <summary>
		/// Take all subjects infos of the teacher
		/// </summary>
		/// <param name="email">Email of the teacher to take</param>
		/// <returns>Subjects</returns>
		Task<Result<Dictionary<string, Dictionary<string, IGrouping<string, string>>>>> TakeTeacherSubjectsInfosAsync(string email);
		/// <summary>
		/// Create teacher
		/// </summary>
		/// <param name="teacher">Model to create a teacher</param>
		/// <returns>New teacher</returns>
		Task<Result<TeacherDto>> CreateTeacherAsync(CreateTeacherModel teacher);
	}
}
