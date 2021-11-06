using System;

namespace Kitias.Providers.Models.Subject
{
	/// <summary>
	/// Model to update subject
	/// </summary>
	public class UpdateSubjectModel
	{
		/// <summary>
		/// Subject name
		/// </summary>
		public string Name { get; init; }
		/// <summary>
		/// Subject type
		/// </summary>
		public string Type { get; init; }
		/// <summary>
		/// Subject time
		/// </summary>
		public TimeSpan? Time { get; init; }
		/// <summary>
		/// Subject date
		/// </summary>
		public DateTime? Date { get; init; }
		/// <summary>
		/// Subject week
		/// </summary>
		public string Week { get; init; }
		/// <summary>
		/// Subject day
		/// </summary>
		public string Day { get; init; }
	}
}
