using System;

namespace Kitias.Persistence.Models
{
	public class Teacher
	{
		public Guid Id { get; set; }
		public Guid PersonId { get; set; }
		public Person Person { get; set; }
	}
}
