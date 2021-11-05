using Microsoft.AspNetCore.Identity;
using System;

namespace Kitias.Persistence.Entities.Identity
{
	/// <summary>
	/// User claim model
	/// </summary>
	public class UserClaim : IdentityUserClaim<Guid>
	{
		/// <summary>
		/// Claim for user
		/// </summary>
		public virtual User User{ get; set; }
	}
}
