using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Kitias.Identity.Server.Models.Entities
{
	public class Role : IdentityRole<Guid>
	{
		public virtual ICollection<RoleClaim> Claims{ get; set; }
		public virtual ICollection<UserRole> UserRoles { get; set; }
	}
}
