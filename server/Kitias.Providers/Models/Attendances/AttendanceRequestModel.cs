using System;

namespace Kitias.Providers.Models.Attendances
{
	/// <summary>
	/// Model to create attendance
	/// </summary>
	public record AttendanceRequestModel
	{
		/// <summary>
		/// Attendance identifier
		/// </summary>
		public Guid Id { get; init; }
		/// <summary>
		/// Flag student is attended
		/// </summary>
		public string Attended { get; init; }
		/// <summary>
		/// Score
		/// </summary>
		public string Score { get; init; }
	}
}
