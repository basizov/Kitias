using System;
using System.Collections.Generic;

namespace Kitias.Identity.Server.Models
{
	public class RegisterModel
	{
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public IEnumerable<Guid> RolesIds { get; set; }
	}
}
