using System;

namespace Kitias.Persistence.DTOs
{
	public record StudentDto
	{
		public Guid Id { get; init; }
		public string Email { get; init; }
		public string FullName { get; init; }
		public byte Course { get; init; }
		public string GroupNumber { get; init; }
		public string EducationType { get; init; }
		public string Speciality { get; init; }
	}
}
