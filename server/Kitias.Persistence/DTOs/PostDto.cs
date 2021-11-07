using System;
using System.Collections.Generic;

namespace Kitias.Persistence.DTOs
{
	/// <summary>
	/// Post data transfer object
	/// </summary>
	public record PostDto
	{
		/// <summary>
		/// Post identifier
		/// </summary>
		public Guid Id { get; init; }
		/// <summary>
		/// Email of the sender
		/// </summary>
		public string OwnerEmail { get; init; }
		/// <summary>
		/// Title of the post
		/// </summary>
		public string Title { get; init; }
		/// <summary>
		/// Description of the post
		/// </summary>
		public string Description { get; init; }
		/// <summary>
		/// Post added date
		/// </summary>
		public string Date { get; init; }
		/// <summary>
		/// Filter to send posts by options
		/// </summary>
		public ICollection<string> Filter { get; init; }

	}
}
