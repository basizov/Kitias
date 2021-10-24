using System;

namespace Kitias.Persistence.Models
{
	public record Teacher
	{
		public Guid Id { get; init; }
		public Guid PersonId { get; init; }
		public Person Person { get; init; }
	}
}
