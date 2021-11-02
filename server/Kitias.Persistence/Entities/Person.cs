using System;

namespace Kitias.Persistence.Entities
{
	public record Person
	{
		public Guid Id { get; init; }
		public string Name { get; init; }
		public string Surname { get; init; }
		public string Patronymic { get; init; }
		public string FullName { get; init; }
		public string Email { get; init; }
	}
}
