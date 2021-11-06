using Kitias.Persistence.DTOs;
using Kitias.Providers.Models;
using Kitias.Providers.Models.Student;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kitias.Providers.Interfaces
{
	/// <summary>
	/// Provider to work with students
	/// </summary>
	public interface IStudentProvider
	{
		/// <summary>
		/// Take all students from db
		/// </summary>
		/// <returns>Students</returns>
		Result<IEnumerable<StudentDto>> TakeStudents();
		/// <summary>
		/// Take only one student from db
		/// </summary>
		/// <param name="id">Id of the student to take</param>
		/// <returns>Student</returns>
		Task<Result<StudentDto>> TakeStudentByIdAsync(Guid id);
		/// <summary>
		/// Create student
		/// </summary>
		/// <param name="student">Model to create a student</param>
		/// <returns>New student</returns>
		Task<Result<StudentDto>> CreateStudentAsync(CreateStudentModel student);
	}
}
