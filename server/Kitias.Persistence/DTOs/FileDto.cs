using System;

namespace Kitias.Persistence.DTOs
{
	/// <summary>
	/// File data transfer object
	/// </summary>
	public record FileDto
	{
		/// <summary>
		/// File identifier
		/// </summary>
		public Guid Id { get; init; }
		/// <summary>
		/// File name and extension
		/// </summary>
		public string FullName { get; init; }
		/// <summary>
		/// File added date
		/// </summary>
		public string Date { get; init; }
		/// <summary>
		/// Email of the sender
		/// </summary>
		public string OwnerEmail { get; init; }
		/// <summary>
		/// Path to local file storing
		/// </summary>
		public string Path { get; init; }
		/// <summary>
		/// File size
		/// </summary>
		public string Size { get; init; }
	}
}
