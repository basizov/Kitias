namespace Kitias.Persistence.Entities.Default
{
	public record ISSecure
	{
		public string Authority { get; init; }
		public string ApiName { get; init; }
		public string ClientId { get; init; }
		public string ClientSecret { get; init; }
	}
}
