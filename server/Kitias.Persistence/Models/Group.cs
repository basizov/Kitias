using Kitias.Persistence.Enums;
using System;
using System.Collections.Generic;

namespace Kitias.Persistence.Models
{
	public record Group
	{
		public Guid Id { get; init; }
		public byte Course { get; init; }
		public string Number { get; init; }
		public EducationType EducationType { get; init; }
		public Speciality Speciality { get; init; }
		public DateTime ReceiptDate { get; init; }
		public DateTime IssueDate { get; init; }
		public ICollection<Student> Students { get; init; }
	}
}
