using System;

namespace Kitias.Persistence.Entities.File
{
	/// <summary>
	/// Entity to store file info
	/// </summary>
	public record File
	{
		/// <summary>
		/// File identifier
		/// </summary>
		public Guid Id { get; init; }
		/// <summary>
		/// File name
		/// </summary>
		public string Name { get; init; }
		/// <summary>
		/// File extension
		/// </summary>
		public string Extension { get; init; }
		/// <summary>
		/// File added date
		/// </summary>
		public DateTime Date { get; init; }
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
		public long Size { get; init; }
	}
}
