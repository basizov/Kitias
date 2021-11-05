using Microsoft.AspNetCore.Identity;
using System;

namespace Kitias.Persistence.Entities.Identity
{
	/// <summary>
	/// Role claim entity
	/// </summary>
	public class RoleClaim : IdentityRoleClaim<Guid>
	{
		/// <summary>
		/// Claim for role
		/// </summary>
		public virtual Role Role { get; set; }
	}
}
