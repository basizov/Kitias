using System;

namespace Kitias.Persistence.Models
{
	public record RefreshToken
	{
		public Guid Id { get; init; }
		public Guid UserId { get; init; }
		public User User { get; init; }
		public string Token { get; init; }
		public string FingerPrint { get; init; }
		public string Ip { get; init; }
		public DateTime Expires { get; init; } = DateTime.UtcNow.AddDays(7);
		public bool IsActive => DateTime.UtcNow >= Expires;
	}
}
