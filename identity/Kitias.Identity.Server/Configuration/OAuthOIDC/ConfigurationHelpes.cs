using System.Collections.Generic;

namespace Kitias.Identity.Server.OAuthOIDC.Configuration
{
	public static partial class Config
	{
		private const string API_RESOURCE = "kitiasApi";
		private const string DATA_ACCESS_SCOPE = "kitiasApi.data";
		private const string IDENTITY_CLAIMS_RESOURCE = "claims";
		private const string KITIAS_API_SECRET = "|||uqpySecret!";

		private static ICollection<T> TakeElements<T>(params T[] list) =>
			new List<T>(list);
	}
}
