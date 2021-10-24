using Microsoft.AspNetCore.Identity;
using System;

namespace Kitias.Identity.Server.Models.Entities
{
	public class UserToken : IdentityUserToken<Guid>
	{
		public virtual User User { get; set; }
		public string Token { get; init; }
		public string FingerPrint { get; init; }
		public string Ip { get; init; }
		public DateTime Expires { get; init; } = DateTime.UtcNow.AddDays(7);
		public bool IsActive => DateTime.UtcNow >= Expires;
	}
}
