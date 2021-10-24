using Microsoft.AspNetCore.Identity;
using System;

namespace Kitias.Identity.Server.Models.Entities
{
	public class RoleClaim : IdentityRoleClaim<Guid>
	{
		public virtual Role Role { get; set; }
	}
}
