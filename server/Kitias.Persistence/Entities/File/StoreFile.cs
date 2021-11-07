using System;

namespace Kitias.Persistence.Entities.File
{
	/// <summary>
	/// Many to many with store and files
	/// </summary>
	public record StoreFile
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
		/// Store identifier
		/// </summary>
		public Guid StoreId { get; init; }
		/// <summary>
		/// Store of the post
		/// </summary>
		public virtual Store Store { get; init; }
	}
}
