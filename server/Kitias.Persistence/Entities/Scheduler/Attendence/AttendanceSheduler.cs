using Kitias.Persistence.Entities.People;
using System;
using System.Collections.Generic;

namespace Kitias.Persistence.Entities.Scheduler.Attendence
{
	/// <summary>
	/// Sheduler of group attendence
	/// </summary>
	public record AttendanceSheduler
	{
		/// <summary>
		/// AttendanceSheduler identifier
		/// </summary>
		public Guid Id { get; init; }
		/// <summary>
		/// Teacher identifier
		/// </summary>
		public Guid TeacherId { get; init; }
		/// <summary>
		/// Teacher info
		/// </summary>
		public Teacher Teacher { get; init; }
		/// <summary>
		/// Group identifier
		/// </summary>
		public Guid GroupId { get; init; }
		/// <summary>
		/// Group info
		/// </summary>
		public Group Group { get; init; }
		/// <summary>
		/// StudentAttendance collection
		/// </summary>
		public ICollection<StudentAttendance> StudentAttendances { get; init; }
	}
}
