namespace Kitias.Identity.Server.Models.ResponseModels
{
	public record TokenResponseModel
	{
		public string AccessToken { get; init; }
		public string RefreshToken { get; init; }
	}
}
