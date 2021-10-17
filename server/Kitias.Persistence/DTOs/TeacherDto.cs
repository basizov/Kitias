using System;

namespace Kitias.Persistence.DTOs
{
	public class TeacherDto
	{
		public Guid Id { get; init; }
		public string Email { get; init; }
		public string FullName { get; init; }
	}
}
