using System.Collections.Generic;

namespace Kitias.Providers.Models.Request
{
	/// <summary>
	/// Model for registration user
	/// </summary>
	public record SignUpRequestModel
	{
		/// <summary>
		/// New user email
		/// </summary>
		public string Email { get; init; }
		/// <summary>
		/// New user username
		/// </summary>
		public string UserName { get; init; }
		/// <summary>
		/// New user password
		/// </summary>
		public string Password { get; init; }
		/// <summary>
		/// User roles
		/// </summary>
		public IEnumerable<string> Roles { get; init; }
	}
}
