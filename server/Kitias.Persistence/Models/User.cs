using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Kitias.Persistence.Models
{
	public class User : IdentityUser<Guid>
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Patronymic { get; set; }
		public string FullName { get; set; }
		public ICollection<RefreshToken> RefreshTokens { get; set; }
	}
}
