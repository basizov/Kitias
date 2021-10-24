namespace Kitias.Identity.Server.Models.DTOs
{
	public record UserDto
	{
		public string UserName { get; init; }
		public string Email { get; init; }
		public string AccessToken { get; init; }
		public string RefreshToken { get; init; }
	}
}
