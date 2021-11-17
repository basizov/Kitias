using System;

namespace Kitias.Persistence.DTOs
{
	/// <summary>
	/// Data transfer object for attendances sheduler
	/// </summary>
	public record AttendanceShedulerDto
	{
		/// <summary>
		/// AttendanceSheduler identifier
		/// </summary>
		public Guid Id { get; init; }
		/// <summary>
		/// Teacher full name
		/// </summary>
		public string TeacherFullName { get; init; }
		/// <summary>
		/// Group number
		/// </summary>
		public string GroupNumber { get; init; }
		/// <summary>
		/// Sheduler name
		/// </summary>
		public string Name { get; init; }
	}
}
