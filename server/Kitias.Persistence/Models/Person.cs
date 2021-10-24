using System;

namespace Kitias.Persistence.Models
{
	public class Person
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Patronymic { get; set; }
		public string FullName { get; set; }
	}
}
