using System;

namespace Kitias.Persistence.Entities
{
	public record Teacher
	{
		public Guid Id { get; init; }
		public Guid PersonId { get; init; }
		public Person Person { get; init; }
	}
}
