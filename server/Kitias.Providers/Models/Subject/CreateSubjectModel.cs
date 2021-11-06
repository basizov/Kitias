using System;
using System.Collections.Generic;

namespace Kitias.Providers.Models.Subject
{
	/// <summary>
	/// Model to create subject
	/// </summary>
	public class CreateSubjectModel
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
		public TimeSpan Time { get; init; }
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
		/// <summary>
		/// Groups which have this subject
		/// </summary>
		public IEnumerable<Guid> GroupIds { get; init; }
	}
}
