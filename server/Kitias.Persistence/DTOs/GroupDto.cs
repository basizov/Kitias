using System;
using System.Collections.Generic;

namespace Kitias.Persistence.DTOs
{
	public record GroupDto
	{
		public Guid Id { get; init; }
		public byte Course { get; init; }
		public string Number { get; init; }
		public string EducationType { get; init; }
		public string Speciality { get; init; }
		public string ReceiptDate { get; init; }
		public string IssueDate { get; init; }
		public ICollection<StudentDto> Students { get; init; }
	}
}
