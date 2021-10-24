using Microsoft.AspNetCore.Identity;
using System;

namespace Kitias.Identity.Server.Models.Entities
{
	public class UserClaim : IdentityUserClaim<Guid>
	{
		public virtual User User{ get; set; }
	}
}
