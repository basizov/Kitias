namespace Kitias.Identity.Server.Models
{
	public class TokenModel
	{
		public string Email { get; set; }
		public string Token { get; init; }
		public string FingerPrint { get; init; }
		public string Ip { get; init; }
	}
}
