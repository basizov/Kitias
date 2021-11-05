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
		public Guid PersonId { get; init; }
		/// <summary>
		/// Person
		/// </summary>
		public Person Person { get; init; }
		/// <summary>
		/// Group id
		/// </summary>
		public Guid GroupId { get; set; }
		/// <summary>
		/// Student group
		/// </summary>
		public Group Group { get; init; }
	}
}
