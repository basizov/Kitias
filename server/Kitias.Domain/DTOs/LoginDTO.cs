namespace Kitias.Domain.DTOs
{
	public record LoginDTO
	{
		public string Email { get; init; }
		public string Password { get; init; }
	}
}
