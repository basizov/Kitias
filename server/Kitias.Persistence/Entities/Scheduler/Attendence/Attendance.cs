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
		/// Date of the pair
		/// </summary>
		public DateTime Date { get; init; }
		/// <summary>
		/// Student identifier
		/// </summary>
		public Guid StudentId { get; init; }
		/// <summary>
		/// Student info
		/// </summary>
		public virtual Student Student { get; init; }
		/// <summary>
		/// Subject identifier
		/// </summary>
		public Guid SubjectId { get; init; }
		/// <summary>
		/// Subject info
		/// </summary>
		public virtual Subject Subject { get; init; }
		/// <summary>
		/// Flag student is attended
		/// </summary>
		public AttendaceVariants Attended { get; init; }
		/// <summary>
		/// Subject theme
		/// </summary>
		public string Theme { get; init; }
		/// <summary>
		/// Score
		/// </summary>
		public byte Score { get; init; }
	}
}
