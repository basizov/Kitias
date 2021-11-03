namespace Kitias.Providers.Models.Request
{
	/// <summary>
	/// Save token model
	/// </summary>
	public record TokenRequestModel
	{
		/// <summary>
		/// Refresh token
		/// </summary>
		public string Token { get; init; }
		/// <summary>
		/// User e-mail
		/// </summary>
		public string UserName{ get; init; }
	}
}
