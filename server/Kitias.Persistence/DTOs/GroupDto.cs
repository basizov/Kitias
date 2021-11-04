using System;
using System.Collections.Generic;

namespace Kitias.Persistence.DTOs
{
	/// <summary>
	/// Group data transfer object
	/// </summary>
	public record GroupDto
	{
		/// <summary>
		/// Group identifier
		/// </summary>
		public Guid Id { get; init; }
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
		/// Group issue date
		/// </summary>
		public string IssueDate { get; init; }
		/// <summary>
		/// Group students
		/// </summary>
		public ICollection<StudentDto> Students { get; init; }
	}
}
