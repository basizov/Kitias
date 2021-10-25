using Microsoft.AspNetCore.Identity;
using System;

namespace Kitias.Identity.Server.Models.Entities
{
	public class UserToken : IdentityUserToken<Guid>
	{
		public Guid Id { get; set; }
		public virtual User User { get; set; }
		public DateTime Expires { get; init; } = DateTime.UtcNow.AddDays(7);
		public bool IsActive => DateTime.UtcNow >= Expires;
	}
}
