using System;

namespace Kitias.Persistence.Models
{
	public record Student
	{
		public Guid Id { get; init; }
		public Guid PersonId { get; init; }
		public Person Person { get; init; }
		public Guid GroupId { get; init; }
		public Group Group { get; init; }
	}
}
