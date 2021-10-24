using System;

namespace Kitias.Persistence.DTOs
{
	public record SubjectDto
	{
		public Guid Id { get; init; }
		public string Name { get; init; }
		public string Type { get; init; }
		public TimeSpan Time { get; init; }
		public DateTime? Date { get; init; }
		public string Week { get; init; }
		public string Day { get; init; }
		public byte Course { get; init; }
		public string GroupNumber { get; init; }
		public string Speciality { get; init; }
	}
}
