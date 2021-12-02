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
		public virtual Teacher Teacher { get; set; }
		/// <summary>
		/// Subject name
		/// </summary>
		public string SubjectName { get; set; }
		/// <summary>
		/// Group identifier
		/// </summary>
		public Guid? GroupId { get; set; }
		/// <summary>
		/// Group info
		/// </summary>
		public virtual Group Group { get; set; }
		/// <summary>
		/// Sheduler name
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Attendance collection
		/// </summary>
		public virtual ICollection<Attendance> Attendances { get; init; }
		/// <summary>
		/// StudentAttendance collection
		/// </summary>
		public virtual ICollection<StudentAttendance> StudentAttendances { get; init; }
	}
}
