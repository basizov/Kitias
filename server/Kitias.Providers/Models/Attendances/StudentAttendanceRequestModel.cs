using System;
using System.Collections.Generic;

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
	}
}
