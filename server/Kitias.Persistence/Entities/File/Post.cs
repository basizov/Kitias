using Kitias.Persistence.Enums;
using System;
using System.Collections.Generic;

namespace Kitias.Persistence.Entities.File
{
	/// <summary>
	/// Entity to store post/news
	/// </summary>
	public record Post
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
		public DateTime Date { get; init; }
		/// <summary>
		/// Filter to send posts by options
		/// </summary>
		public ICollection<PersonRoles> Filter { get; init; }
		/// <summary>
		/// Post files
		/// </summary>
		public virtual ICollection<FilePost> Files { get; init; }
	}
}
