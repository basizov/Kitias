namespace Kitias.Identity.Server.Models.RequestModels
{
	public record TokenRequestModel
	{
		public string ClientId { get; init; }
		public string ClientSecret { get; init; }
	}
}
