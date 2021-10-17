using System;

namespace Kitias.Persistence.Models
{
	public class Teacher
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public User User { get; set; }
	}
}
