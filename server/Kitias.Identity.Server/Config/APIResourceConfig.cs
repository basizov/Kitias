using IdentityServer4.Models;
using System.Collections.Generic;

namespace Kitias.Identity.Server.Config
{
	/// <summary>
	/// Config for OAuth 2.0 and Open Id
	/// </summary>
	public static partial class OAuthOIDCConfig
	{
		/// <summary>
		/// Initial api resources
		/// </summary>
		public static IEnumerable<ApiResource> TakeAPIResources = new List<ApiResource>
		{
			new()
			{
				Name = KITIAS_RESOURCE,
				DisplayName = "Kitias",
				Description = "Allow the application to access Kitias on your behalf",
				Scopes = new List<string>
				{
					"kitias.data"
				},
				ApiSecrets = new List<Secret>
				{
					new(KITIAS_API_SECRET.Sha256())
				}
			}
		};
	}
}
