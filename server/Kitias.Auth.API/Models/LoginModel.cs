namespace Kitias.Auth.API.Models
{
	public record LoginModel
	{
		public string Email { get; init; }
		public string Password { get; init; }
	}
}
