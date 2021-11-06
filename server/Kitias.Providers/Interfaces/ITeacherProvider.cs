using Kitias.Persistence.DTOs;
using Kitias.Providers.Models;
using Kitias.Providers.Models.Person;
using System;
using System.Collections.Generic;
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
		/// Create teacher
		/// </summary>
		/// <param name="teacher">Model to create a teacher</param>
		/// <returns>New teacher</returns>
		Task<Result<TeacherDto>> CreateTeacherAsync(CreateTeacherModel teacher);
	}
}
