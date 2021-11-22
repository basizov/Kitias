using Kitias.Persistence.Entities.Scheduler;
using Kitias.Persistence.Entities.Scheduler.Attendence;
using System;
using System.Collections.Generic;

namespace Kitias.Persistence.Entities.People
{
	/// <summary>
	/// Teacher entity
	/// </summary>
	public record Teacher
	{
		/// <summary>
		/// Teacher identifier
		/// </summary>
		public Guid Id { get; init; }
		/// <summary>
		/// Teacher personnility id
		/// </summary>
		public Guid PersonId { get; set; }
		/// <summary>
		/// Teacher personality
		/// </summary>
		public virtual Person Person { get; set; }
		/// <summary>
		/// Teacher shedulers
		/// </summary>
		public virtual ICollection<AttendanceSheduler> Shedulers { get; init; }
		/// <summary>
		/// Subjects
		/// </summary>
		public virtual ICollection<Subject> Subjects { get; init; }
	}
}
