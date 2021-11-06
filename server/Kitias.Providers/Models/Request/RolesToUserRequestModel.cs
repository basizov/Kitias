using System.Collections.Generic;

namespace Kitias.Providers.Models.Request
{
	/// <summary>
	/// Add roles to user
	/// </summary>
	public record RolesToUserRequestModel
	{
		/// <summary>
		/// User email
		/// </summary>
		public string Email { get; init; }
		/// <summary>
		/// New roles
		/// </summary>
		public IEnumerable<string> Roles { get; init; }
	}
}
