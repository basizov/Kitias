using System;
using System.Collections.Generic;

namespace Kitias.Persistence.DTOs
{
	public class GroupDto
	{
		public Guid Id { get; set; }
		public byte Course { get; set; }
		public string Number { get; set; }
		public string EducationType { get; set; }
		public string Speciality { get; set; }
		public DateTime ReceiptDate { get; set; }
		public DateTime IssueDate { get; set; }
		public ICollection<StudentDto> Students { get; set; }
	}
}
