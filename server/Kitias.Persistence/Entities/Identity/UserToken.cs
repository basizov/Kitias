using Microsoft.AspNetCore.Identity;
using System;

namespace Kitias.Persistence.Entities.Identity
{
	/// <summary>
	/// Token entity
	/// </summary>
	public class UserToken : IdentityUserToken<Guid>
	{
		/// <summary>
		/// Token identifier
		/// </summary>
		public Guid Id { get; set; }
		/// <summary>
		/// Token for user
		/// </summary>
		public virtual User User { get; set; }
		/// <summary>
		/// Token expires date
		/// </summary>
		public DateTime Expires { get; set; } = DateTime.UtcNow.AddDays(7);
		/// <summary>
		/// True if token is active
		/// </summary>
		public bool IsActive => DateTime.UtcNow >= Expires;
	}
}
