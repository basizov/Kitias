using System;

namespace Kitias.Persistence.Models
{
	public class Student
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public User User { get; set; }
		public Guid GroupId { get; set; }
		public Group Group { get; set; }
	}
}
