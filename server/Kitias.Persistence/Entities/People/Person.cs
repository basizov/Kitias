using System;

namespace Kitias.Persistence.Entities.People
{
	/// <summary>
	/// Person entity
	/// </summary>
	public record Person
	{
		/// <summary>
		/// Person identifier
		/// </summary>
		public Guid Id { get; init; }
		/// <summary>
		/// Person name
		/// </summary>
		public string Name { get; init; }
		/// <summary>
		/// Person surname
		/// </summary>
		public string Surname { get; init; }
		/// <summary>
		/// Person patronymic
		/// </summary>
		public string Patronymic { get; init; }
		/// <summary>
		/// Person fullname
		/// </summary>
		public string FullName { get; init; }
		/// <summary>
		/// Person email
		/// </summary>
		public string Email { get; init; }
	}
}
