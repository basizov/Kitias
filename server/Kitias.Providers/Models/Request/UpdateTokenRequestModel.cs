namespace Kitias.Providers.Models.Request
{
	/// <summary>
	/// Model to update old token
	/// </summary>
	public record UpdateTokenRequestModel
	{
		/// <summary>
		/// Old token
		/// </summary>
		public string OldToken { get; init; }
		/// <summary>
		/// New token
		/// </summary>
		public string NewToken { get; init; }
	}
}
