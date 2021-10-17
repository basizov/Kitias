using System;

namespace Kitias.Persistence.DTOs
{
	public class SubjectDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public TimeSpan Time { get; set; }
		public DateTime? Date { get; set; }
		public string Week { get; set; }
		public string Day { get; set; }
		public byte Course { get; set; }
		public string GroupNumber { get; set; }
		public string Speciality { get; set; }
	}
}
