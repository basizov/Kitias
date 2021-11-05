using Microsoft.AspNetCore.Identity;
using System;

namespace Kitias.Persistence.Entities.Identity
{
	/// <summary>
	/// User role entity
	/// </summary>
	public class UserRole : IdentityUserRole<Guid>
	{
		/// <summary>
		/// UserRole for user 
		/// </summary>
		public virtual User User { get; set; }
		/// <summary>
		/// UserRole for role
		/// </summary>
		public virtual Role Role{ get; set; }
	}
}
