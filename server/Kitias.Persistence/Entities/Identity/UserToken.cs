using Microsoft.AspNetCore.Identity;
using System;

namespace Kitias.Persistence.Entities.Identity
{
	public class UserToken : IdentityUserToken<Guid>
	{
		public Guid Id { get; set; }
		public virtual User User { get; set; }
		public DateTime Expires { get; set; } = DateTime.UtcNow.AddDays(7);
		public bool IsActive => DateTime.UtcNow >= Expires;
	}
}
