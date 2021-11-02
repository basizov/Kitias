namespace Kitias.Providers.Models
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
	}
}
