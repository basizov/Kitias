namespace Kitias.Providers.Models.Attendances
{
	/// <summary>
	/// Model to update student attendance
	/// </summary>
	public record UpdateStudentAttendanceModel
	{
		/// <summary>
		/// Student raiting
		/// </summary>
		public string Raiting { get; init; }
		/// <summary>
		/// Student grade
		/// </summary>
		public string Grade { get; init; }
	}
}
