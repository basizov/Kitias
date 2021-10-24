using System;
using System.Collections.Generic;

namespace Kitias.API.Models
{
	public record RegisterModel
	{
		public string UserName { get; init; }
		public string Email { get; init; }
		public string Password { get; init; }
		public string Name { get; init; }
		public string Surname { get; init; }
		public string Patronymic { get; init; }
		public IEnumerable<Guid> RolesIds { get; init; }
	}
}
