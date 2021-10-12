namespace Kitias.API.Models
{
	public record RegisterModel
	{
		public string Email { get; init; }
		public string Password { get; init; }
	}
}
