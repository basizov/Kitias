namespace Kitias.Providers.Models.Request
{
	/// <summary>
	/// Sign in model
	/// </summary>
	public record SignInRequestModel
	{
		/// <summary>
		/// User username
		/// </summary>
		public string UserName { get; init; }
		/// <summary>
		/// User password
		/// </summary>
		public string Password { get; init; }
	}
}
