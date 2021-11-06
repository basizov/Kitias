using System;

namespace Kitias.Persistence.Entities
{
	/// <summary>
	/// Student entity
	/// </summary>
	public record Student
	{
		/// <summary>
		/// Student identifier
		/// </summary>
		public Guid Id { get; init; }
		/// <summary>
		/// Person id
		/// </summary>
		public Guid PersonId { get; set; }
		/// <summary>
		/// Person
		/// </summary>
		public virtual Person Person { get; set; }
		/// <summary>
		/// Group id
		/// </summary>
		public Guid GroupId { get; set; }
		/// <summary>
		/// Student group
		/// </summary>
		public virtual Group Group { get; set; }
	}
}
