namespace Kitias.Auth.API.Models
{
	public record RegisterModel
	{
		public string Email { get; init; }
		public string Password { get; init; }
		public string Name { get; init; }
		public string Surname { get; init; }
		public string Patronymic { get; init; }
	}
}
