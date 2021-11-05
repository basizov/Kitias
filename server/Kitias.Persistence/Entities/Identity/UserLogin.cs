using Microsoft.AspNetCore.Identity;
using System;

namespace Kitias.Persistence.Entities.Identity
{
	/// <summary>
	/// User login model
	/// </summary>
	public class UserLogin : IdentityUserLogin<Guid>
	{
		/// <summary>
		/// Login for user
		/// </summary>
		public virtual User User { get; set; }
	}
}
