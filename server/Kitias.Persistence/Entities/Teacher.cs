using System;

namespace Kitias.Persistence.Entities
{
	/// <summary>
	/// Teacher entity
	/// </summary>
	public record Teacher
	{
		/// <summary>
		/// Teacher identifier
		/// </summary>
		public Guid Id { get; init; }
		/// <summary>
		/// Teacher personnility id
		/// </summary>
		public Guid PersonId { get; init; }
		/// <summary>
		/// Teacher personality
		/// </summary>
		public Person Person { get; init; }
	}
}
