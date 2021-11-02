namespace Kitias.Providers.Models.Request
{
	public record SignInRequestModel
	{
		public string UserName { get; init; }
		public string Password { get; init; }
	}
}
