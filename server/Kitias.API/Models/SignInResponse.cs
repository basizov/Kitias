namespace Kitias.API.Models
{
	public record SignInResponse
	{
		public string UserName { get; init; }
		public string Email { get; init; }
		public string AccessToken { get; init; }
		public string RefreshToken { get; init; }
	}
}
