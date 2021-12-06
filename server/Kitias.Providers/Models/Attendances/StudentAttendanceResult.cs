using System;
using System.Collections.Generic;

namespace Kitias.Providers.Models.Attendances
{
	/// <summary>
	/// Result of student attendace
	/// </summary>
	public record StudentAttendanceResult
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
		/// Student lectures result
		/// </summary>
		public IEnumerable<SubjectTypeInfo> Lectures { get; init; }
		/// <summary>
		/// Student laborotories result
		/// </summary>
		public IEnumerable<SubjectTypeInfo> Laborotories { get; init; }
		/// <summary>
		/// Student practises result
		/// </summary>
		public IEnumerable<SubjectTypeInfo> Practises { get; init; }
	}

	/// <summary>
	/// Type info of the subjct
	/// </summary>
	public record SubjectTypeInfo
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
