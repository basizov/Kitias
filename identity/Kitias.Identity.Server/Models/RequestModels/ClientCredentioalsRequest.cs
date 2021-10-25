namespace Kitias.Identity.Server.Models.RequestModels
{
	public record ClientCredentioalsRequest
	{
		public string ClientId { get; init; }
		public string ClientSecret { get; init; }
	}
}
