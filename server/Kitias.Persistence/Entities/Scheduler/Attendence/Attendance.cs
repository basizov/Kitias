using Kitias.Persistence.Entities.People;
using Kitias.Persistence.Enums;
using System;

namespace Kitias.Persistence.Entities.Scheduler.Attendence
{
	/// <summary>
	/// Attendance of subjects
	/// </summary>
	public record Attendance
	{
		/// <summary>
		/// Attendance identifier
		/// </summary>
		public Guid Id { get; init; }
		/// <summary>
		/// Sheduler identifier
		/// </summary>
		public Guid ShedulerId { get; init; }
		/// <summary>
		/// Sheduler info
		/// </summary>
		public AttendanceSheduler Sheduler { get; init; }
		/// <summary>
		/// Student identifier
		/// *** Null if studentName != null
		/// </summary>
		public Guid? StudentId { get; set; }
		/// <summary>
		/// Student name
		/// *** Null if studentId != null
		/// </summary>
		public string StudentName { get; set; }
		/// <summary>
		/// Student info
		/// </summary>
		public virtual Student Student { get; set; }
		/// <summary>
		/// Subject identifier
		/// </summary>
		public Guid SubjectId { get; set; }
		/// <summary>
		/// Subject info
		/// </summary>
		public virtual Subject Subject { get; set; }
		/// <summary>
		/// Flag student is attended
		/// </summary>
		public AttendaceVariants Attended { get; set; }
		/// <summary>
		/// Score
		/// </summary>
		public byte Score { get; set; }
	}
}
