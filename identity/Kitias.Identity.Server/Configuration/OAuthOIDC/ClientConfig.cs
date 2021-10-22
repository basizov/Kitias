using IdentityServer4.Models;
using System.Collections.Generic;
using static IdentityServer4.IdentityServerConstants;

namespace Kitias.Identity.Server.OAuthOIDC.Configuration
{
	public static partial class Config
	{
		public static IEnumerable<Client> GetClients => TakeElements
		(
			new Client
			{
				ClientId = "Kitias.API",
				ClientName = "ASP.Net Core Kitias Web Api",
				AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
				ClientSecrets = TakeElements(new Secret(KITIAS_API_SECRET.Sha256())),
				AllowedScopes = TakeElements(DATA_ACCESS_SCOPE),
				AllowOfflineAccess = true
			}
		);
	}
}
