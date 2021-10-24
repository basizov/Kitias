using System;

namespace Kitias.Persistence.Models
{
	public class Student
	{
		public Guid Id { get; set; }
		public Guid PersonId { get; set; }
		public Person Person { get; set; }
		public Guid GroupId { get; set; }
		public Group Group { get; set; }
	}
}
