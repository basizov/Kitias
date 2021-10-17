using System;

namespace Kitias.Persistence.DTOs
{
	public class StudentDto
	{
		public Guid Id { get; init; }
		public string Email { get; init; }
		public string FullName { get; init; }
		public byte Course { get; set; }
		public string GroupNumber { get; set; }
		public string EducationType { get; set; }
		public string Speciality { get; set; }
	}
}
