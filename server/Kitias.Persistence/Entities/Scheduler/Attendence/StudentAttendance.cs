using Kitias.Persistence.Entities.People;
using Kitias.Persistence.Enums;
using System;
using System.Collections.Generic;

namespace Kitias.Persistence.Entities.Scheduler.Attendence
{
	/// <summary>
	/// Shecduler of student attendence
	/// </summary>
	public record StudentAttendance
	{
		/// <summary>
		/// StudentAttendance identifier
		/// </summary>
		public Guid Id { get; init; }
		/// <summary>
		/// Sheduler identifier
		/// </summary>
		public Guid ShedulerId { get; set; }
		/// <summary>
		/// Sheduler info
		/// </summary>
		public virtual AttendanceSheduler Sheduler { get; set; }
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
		/// Subject name
		/// </summary>
		public string SubjectName { get; set; }
		/// <summary>
		/// Student raiting
		/// </summary>
		public byte Raiting{ get; set; }
		/// <summary>
		/// Student grade
		/// </summary>
		public Grade Grade { get; set; }
		/// <summary>
		/// Student flag to modificate grade
		/// </summary>
		public bool IsAutomatic { get; set; } = true;
	}
}
