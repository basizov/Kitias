using System;

namespace Kitias.Persistence.DTOs
{
	/// <summary>
	/// Data transfer object for attendance
	/// </summary>
	public record AttendanceDto
	{
		/// <summary>
		/// Attendance identifier
		/// </summary>
		public Guid Id { get; init; }
		/// <summary>
		/// Date of the pair
		/// </summary>
		public string Date { get; init; }
		/// <summary>
		/// True if student is attended
		/// </summary>
		public bool IsAttended { get; init; }
		/// <summary>
		/// Subject theme
		/// </summary>
		public string Theme { get; init; }
		/// <summary>
		/// Score
		/// </summary>
		public string Score { get; init; }
		/// <summary>
		/// Subject type
		/// </summary>
		public string Type { get; set; }
	}
}
