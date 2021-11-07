using Kitias.Persistence.Enums;
using System;
using System.Collections.Generic;

namespace Kitias.Persistence.Entities.Scheduler
{
	/// <summary>
	/// Subject entity
	/// </summary>
	public record Subject
	{
		/// <summary>
		/// Subject identifier
		/// </summary>
		public Guid Id { get; init; }
		/// <summary>
		/// Subject name
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Subject type
		/// </summary>
		public SubjectType Type { get; set; }
		/// <summary>
		/// Subject time
		/// </summary>
		public TimeSpan Time { get; set; }
		/// <summary>
		/// Subject date
		/// </summary>
		public DateTime? Date { get; set; }
		/// <summary>
		/// Subject week
		/// </summary>
		public Week Week { get; set; }
		/// <summary>
		/// Subject day
		/// </summary>
		public DayWeek Day { get; set; }
		/// <summary>
		/// Subject groups
		/// </summary>
		public virtual ICollection<SubjectGroup> Groups { get; init; }
	}
}
