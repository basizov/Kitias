using System;

namespace Kitias.Persistence.Entities.File
{
	/// <summary>
	/// Many to many woth posts and files
	/// </summary>
	public record FilePost
	{
		/// <summary>
		/// FilePost identifier
		/// </summary>
		public Guid Id { get; init; }
		/// <summary>
		/// Post identifier
		/// </summary>
		public Guid PostId { get; init; }
		/// <summary>
		/// Post of the file
		/// </summary>
		public virtual Post Post { get; init; }
		/// <summary>
		/// File identifier
		/// </summary>
		public Guid FileId { get; init; }
		/// <summary>
		/// File of the post
		/// </summary>
		public virtual File File { get; init; }
	}
}
