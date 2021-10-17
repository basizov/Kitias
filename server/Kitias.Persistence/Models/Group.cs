using Kitias.Persistence.Enums;
using System;
using System.Collections.Generic;

namespace Kitias.Persistence.Models
{
	public class Group
	{
		public Guid Id { get; set; }
		public byte Course { get; set; }
		public string Number { get; set; }
		public EducationType EducationType { get; set; }
		public Speciality Speciality { get; set; }
		public DateTime ReceiptDate { get; set; }
		public DateTime IssueDate { get; set; }
		public ICollection<Student> Students { get; set; }
	}
}
