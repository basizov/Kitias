using System;

namespace Kitias.Persistence.DTOs
{
	/// <summary>
	/// Darta transfer object for student attendance
	/// </summary>
	public record StudentAttendanceDto
	{
		/// <summary>
		/// StudentAttendance identifier
		/// </summary>
		public Guid Id { get; init; }
		/// <summary>
		/// Student name
		/// </summary>
		public string StudentName { get; init; }
		/// <summary>
		/// Student raiting
		/// </summary>
		public string Raiting { get; init; }
		/// <summary>
		/// Student grade
		/// </summary>
		public string Grade { get; init; }
		/// <summary>
		/// Subject name
		/// </summary>
		public string SubjectName { get; set; }
	}
}
