namespace Kitias.Providers.Models.Attendances
{
	/// <summary>
	/// Model to update student attendance
	/// </summary>
	public record UpdateAttendanceModel
	{
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
