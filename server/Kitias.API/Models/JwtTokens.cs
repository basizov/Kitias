namespace Kitias.API.Models
{
	public record JwtTokens
	{
		public string AccessToken { get; init; }
		public string RefreshToken { get; init; }
	}
}
