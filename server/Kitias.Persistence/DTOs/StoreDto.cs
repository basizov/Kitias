using System;

namespace Kitias.Persistence.DTOs
{
	/// <summary>
	/// Store data transfer object
	/// </summary>
	public record StoreDto
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
		/// Actual Size __ / Max Size __
		/// </summary>
		public string Size { get; init; }
	}
}
