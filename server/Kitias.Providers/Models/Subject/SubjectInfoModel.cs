using System.Collections.Generic;

namespace Kitias.Providers.Models.Subject
{
	/// <summary>
	/// Subjects info model
	/// </summary>
	public record SubjectInfoModel
	{
		/// <summary>
		/// Name of the subject
		/// </summary>
		public string Name { get; init; }
		/// <summary>
		/// Type of the subject
		/// </summary>
		public string Type { get; init; }
		/// <summary>
		/// List of dates
		/// </summary>
		public string Date { get; init; }
		/// <summary>
		/// List of times
		/// </summary>
		public string Time { get; init; }
	}
}
