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
		/// Teacher identifier
		/// </summary>
		public Guid TeacherId{ get; init; }
		/// <summary>
		/// Teacher info
		/// </summary>
		public virtual Teacher Teacher{ get; init; }
		/// <summary>
		/// Student identifier
		/// </summary>
		public Guid StudentId { get; init; }
		/// <summary>
		/// Student info
		/// </summary>
		public virtual Student Student { get; init; }
		/// <summary>
		/// Student raiting
		/// </summary>
		public byte Raiting{ get; init; }
		/// <summary>
		/// Student grade
		/// </summary>
		public Grade Grade { get; init; }
		/// <summary>
		/// Attendance collection
		/// </summary>
		public virtual ICollection<Attendance> Attendances { get; init; }
	}
}
