using Kitias.Persistence.DTOs;
using Kitias.Providers.Models;
using Kitias.Providers.Models.Attendances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitias.Providers.Interfaces
{
	/// <summary>
	/// Interfsce to work with attendances
	/// </summary>
	public interface IAttendanceProvider
	{
		/// <summary>
		/// Take shedulers of the teacher
		/// </summary>
		/// <param name="email">Teacher email</param>
		/// <returns>List shedulers info</returns>
		Task<Result<IEnumerable<ShedulersListResult>>> TakeTeacherShedulersAsync(string email);
		/// <summary>
		/// Take sheduler of the teacher
		/// </summary>
		/// <param name="id">Identifier of sheduler</param>
		/// <returns>List shedulers info</returns>
		Task<Result<ShedulersListResult>> TakeTeacherShedulerAsync(Guid id);
		/// <summary>
		/// Take shedulersubjects of the teacher
		/// </summary>
		/// <param name="id">Identifier of sheduler</param>
		/// <returns>List shedulers subjects info</returns>
		Task<Result<IEnumerable<SubjectDto>>> TakeTeacherShedulerSubjectsAsync(Guid id);
		/// <summary>
		/// Take attendances of the sheduler
		/// </summary>
		/// <param name="id">Identifier of sheduler</param>
		/// <returns>List attandances</returns>
		Task<Result<Dictionary<string, IGrouping<string, AttendanceDto>>>> TakeShedulerAttendancesAsync(Guid id);
		/// <summary>
		/// Take student attendances of the sheduler
		/// </summary>
		/// <param name="id">Identifier of sheduler</param>
		/// <returns>List student attandances</returns>
		Task<Result<IEnumerable<StudentAttendanceDto>>> TakeShedulerStudentAttendancesAsync(Guid id);
		/// <summary>
		/// Create sheduler for teacher
		/// </summary>
		/// <param name="model">Model to create scheduler</param>
		/// <returns>Created sheduler</returns>
		Task<Result<AttendanceShedulerDto>> CreateShedulerAsync(ShedulerProviderRequestModel model);
		/// <summary>
		/// Update sheduler for teacher
		/// </summary>
		/// <param name="id">Identifier of sheduler</param>
		/// <param name="model">Model to update scheduler</param>
		/// <returns>Updated sheduler</returns>
		Task<Result<AttendanceShedulerDto>> UpdateShedulerAsync(Guid id, ShedulerRequestModel model);
		/// <summary>
		/// Delete sheduler for teacher
		/// </summary>
		/// <param name="id">Identifier of sheduler</param>
		/// <returns>Status message</returns>
		Task<Result<string>> DeleteShedulerAsync(Guid id);
		/// <summary>
		/// Create student attendance
		/// </summary>
		/// <param name="id">Identifier of sheduler</param>
		/// <param name="models">Models to create student attendances</param>
		/// <returns>List of created student attendances</returns>
		Task<Result<IEnumerable<StudentAttendanceDto>>> CreateStudentAttendanceAsync(Guid id, IEnumerable<StudentAttendanceRequestModel> models);
		/// <summary>
		/// Update student attendance
		/// </summary>
		/// <param name="id">Identifier of student attendance</param>
		/// <param name="model">Model to update student attendance</param>
		/// <returns>Updated student attendance</returns>
		Task<Result<StudentAttendanceDto>> UpdateStudentAttendanceAsync(Guid id, UpdateStudentAttendanceModel model);
		/// <summary>
		/// Delete student attendance for teacher
		/// </summary>
		/// <param name="id">Identifier of student attendance</param>
		/// <returns>Status message</returns>
		Task<Result<string>> DeleteStudentAttendanceAsync(Guid id);
		/// <summary>
		/// Create attendance
		/// </summary>
		/// <param name="id">Identifier of sheduler</param>
		/// <param name="models">Models to create attendances</param>
		/// <returns>List of created attendances</returns>
		Task<Result<IEnumerable<AttendanceDto>>> CreateAttendancesAsync(Guid id, IEnumerable<AttendanceRequestModel> models);
		/// <summary>
		/// Update attendance
		/// </summary>
		/// <param name="id">Identifier of attendance</param>
		/// <param name="model">Model to update attendance</param>
		/// <returns>Updated attendance</returns>
		Task<Result<AttendanceDto>> UpdateAttendanceAsync(Guid id, UpdateAttendanceModel model);
		/// <summary>
		/// Delete attendance for teacher
		/// </summary>
		/// <param name="id">Identifier of attendance</param>
		/// <returns>Status message</returns>
		Task<Result<string>> DeleteAttendanceAsync(Guid id);
	}
}
