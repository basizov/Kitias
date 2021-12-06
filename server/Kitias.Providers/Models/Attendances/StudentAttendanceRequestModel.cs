using System;

namespace Kitias.Providers.Models.Attendances
{
	/// <summary>
	/// Model to create student attendance
	/// </summary>
	public record StudentAttendanceRequestModel
	{
		/// <summary>
		/// Student raiting
		/// </summary>
		public string Raiting { get; init; }
		/// <summary>
		/// Student grade
		/// </summary>
		public string Grade { get; init; }
		/// <summary>
		/// Student came from db
		/// </summary>
		public Guid? StudentId { get; init; }
		/// <summary>
		/// Students name
		/// </summary>
		public string StudentName { get; init; }
		/// <summary>
		/// Subject name
		/// </summary>
		public string SubjectName { get; set; }
	}
}
