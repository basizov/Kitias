using Kitias.Persistence.Enums;
using System;

namespace Kitias.Persistence.Entities
{
	public record Subject
	{
		public Guid Id { get; init; }
		public Guid GroupId { get; init; }
		public Group Group { get; init; }
		public string Name { get; init; }
		public SubjectType Type { get; init; }
		public TimeSpan Time { get; init; }
		public DateTime? Date { get; init; }
		public Week Week { get; init; }
		public DayWeek Day { get; init; }
	}
}
