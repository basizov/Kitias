using System;

namespace Kitias.Persistence.DTOs
{
	/// <summary>
	/// Subject data transfer object
	/// </summary>
	public record SubjectDto
	{
		/// <summary>
		/// Subject identifier
		/// </summary>
		public Guid Id { get; init; }
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
		/// Subject course
		/// </summary>
		public byte Course { get; init; }
		/// <summary>
		/// Subject group number
		/// </summary>
		public string GroupNumber { get; init; }
		/// <summary>
		/// Subject speciality
		/// </summary>
		public string Speciality { get; init; }
	}
}
