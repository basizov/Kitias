using Microsoft.AspNetCore.Identity;
using System;

namespace Kitias.Identity.Server.Models.Entities
{
	public class UserLogin : IdentityUserLogin<Guid>
	{
		public virtual User User { get; set; }
	}
}
