using Kitias.Persistence.Enums;
using System;

namespace Kitias.Persistence.Models
{
	public class Subject
	{
		public Guid Id { get; set; }
		public Guid GroupId { get; set; }
		public Group Group { get; set; }
		public string Name { get; set; }
		public SubjectType Type { get; set; }
		public TimeSpan Time { get; set; }
		public DateTime? Date { get; set; }
		public Week Week { get; set; }
		public DayWeek Day { get; set; }
	}
}
