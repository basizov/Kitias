using System;

namespace Kitias.Persistence.DTOs
{
	public record TeacherDto
	{
		public Guid Id { get; init; }
		public string Email { get; init; }
		public string FullName { get; init; }
	}
}
