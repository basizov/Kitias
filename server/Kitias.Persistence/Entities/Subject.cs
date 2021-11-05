using Kitias.Persistence.Enums;
using System;

namespace Kitias.Persistence.Entities
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
		/// Subject group id
		/// </summary>
		public Guid GroupId { get; init; }
		/// <summary>
		/// Subject group
		/// </summary>
		public Group Group { get; init; }
		/// <summary>
		/// Subject name
		/// </summary>
		public string Name { get; init; }
		/// <summary>
		/// Subject type
		/// </summary>
		public SubjectType Type { get; init; }
		/// <summary>
		/// Subject time
		/// </summary>
		public TimeSpan Time { get; init; }
		/// <summary>
		/// Subject date
		/// </summary>
		public DateTime? Date { get; init; }
		/// <summary>
		/// Subject week
		/// </summary>
		public Week Week { get; init; }
		/// <summary>
		/// Subject day
		/// </summary>
		public DayWeek Day { get; init; }
	}
}
