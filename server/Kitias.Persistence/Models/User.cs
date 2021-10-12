using Microsoft.AspNetCore.Identity;
using System;

namespace Kitias.Persistence.Models
{
	public class User : IdentityUser<Guid>
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Patronymic { get; set; }
	}
}
