using System;

namespace Kitias.Providers.Models.Attendances
{
	/// <summary>
	/// Model to create attendance
	/// </summary>
	public record AttendanceRequestModel
	{
		/// <summary>
		/// Flag student is attended
		/// </summary>
		public string Attended { get; init; }
		/// <summary>
		/// Score
		/// </summary>
		public string Score { get; init; }
		/// <summary>
		/// Student came from db
		/// </summary>
		public Guid? StudentId { get; init; }
		/// <summary>
		/// Students name
		/// </summary>
		public string StudentName { get; init; }
		/// <summary>
		/// Subject identifier
		/// </summary>
		public Guid SubjectId { get; set; }
	}
}
