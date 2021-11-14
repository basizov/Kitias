using Kitias.Persistence.DTOs;
using Kitias.Providers.Models;
using System;
using System.Collections.Generic;
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
		/// <returns>List shedulers ids</returns>
		Task<Result<IEnumerable<Guid>>> TakeTeacherShedulersAsync(string email);
		/// <summary>
		/// Take attendances of the sheduler
		/// </summary>
		/// <param name="id">Identifier of attendace</param>
		/// <returns>List attandances</returns>
		Task<Result<IEnumerable<AttendanceDto>>> TakeShedulerAttendancesAsync(Guid id);
	}
}
