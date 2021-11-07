using System;
using System.Collections.Generic;

namespace Kitias.Persistence.Entities.File
{
	/// <summary>
	/// Place to store own files
	/// </summary>
	public record Store
	{
		/// <summary>
		/// Store identifier
		/// </summary>
		public Guid Id { get; init; }
		/// <summary>
		/// Owner of storing place
		/// </summary>
		public string OwnerEmail { get; init; }
		/// <summary>
		/// Owner of storing place actual size
		/// </summary>
		public long ActualSize { get; init; }
		/// <summary>
		/// Owner of storing place max size
		/// </summary>
		public long MaxSize { get; init; }
		/// <summary>
		/// Own files
		/// </summary>
		public virtual ICollection<StoreFile> Files { get; init; }
	}
}
