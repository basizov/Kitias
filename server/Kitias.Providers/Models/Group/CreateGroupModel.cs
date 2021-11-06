using System;
using System.Collections.Generic;

namespace Kitias.Providers.Models.Group
{
	/// <summary>
	/// Group model to create new instance
	/// </summary>
	public record CreateGroupModel
	{
		/// <summary>
		/// Group course
		/// </summary>
		public byte Course { get; init; }
		/// <summary>
		/// Group number
		/// </summary>
		public string Number { get; init; }
		/// <summary>
		/// Group education type
		/// </summary>
		public string EducationType { get; init; }
		/// <summary>
		/// Group speciality
		/// </summary>
		public string Speciality { get; init; }
		/// <summary>
		/// Group receipt date
		/// </summary>
		public string ReceiptDate { get; init; }
		/// <summary>
		/// Grou issue date
		/// </summary>
		public string IssueDate { get; init; }
	}
}
