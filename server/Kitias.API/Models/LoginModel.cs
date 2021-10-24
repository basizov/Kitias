namespace Kitias.API.Models
{
	public record LoginModel
	{
		public string UserName { get; init; }
		public string Password { get; init; }
	}
}
