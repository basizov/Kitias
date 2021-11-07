using System;

namespace Kitias.Persistence.Entities.People
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
		public Guid PersonId { get; set; }
		/// <summary>
		/// Teacher personality
		/// </summary>
		public virtual Person Person { get; set; }
	}
}
