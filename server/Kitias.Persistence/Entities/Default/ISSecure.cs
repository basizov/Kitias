namespace Kitias.Persistence.Entities.Default
{
	/// <summary>
	/// Secure entity
	/// </summary>
	public record ISSecure
	{
		/// <summary>
		/// Authority url
		/// </summary>
		public string Authority { get; init; }
		/// <summary>
		/// Audience name
		/// </summary>
		public string ApiName { get; init; }
		/// <summary>
		/// Client id
		/// </summary>
		public string ClientId { get; init; }
		/// <summary>
		/// Client secret
		/// </summary>
		public string ClientSecret { get; init; }
	}
}
