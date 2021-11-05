using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Kitias.Persistence.Entities.Identity
{
	/// <summary>
	/// Role entity
	/// </summary>
	public class Role : IdentityRole<Guid>
	{
		/// <summary>
		/// Collection of claims
		/// </summary>
		public virtual ICollection<RoleClaim> Claims { get; set; }
		/// <summary>
		/// Collection of user roles
		/// </summary>
		public virtual ICollection<UserRole> UserRoles { get; set; }
	}
}
