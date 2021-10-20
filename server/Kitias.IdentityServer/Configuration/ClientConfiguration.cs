using IdentityServer4.Models;
using System.Collections.Generic;

namespace Kitias.IdentityServer.Configuration
{
	public static partial class Configuration
	{
		public static IEnumerable<Client> GetClients => TakeElements
		(
			new Client
			{
				ClientId = "Kitias.API",
				ClientName = "ASP.Net Core Kitias Web Api",
				AllowedGrantTypes = GrantTypes.ClientCredentials,
				ClientSecrets = TakeElements(new Secret(KITIAS_API_SECRET.Sha256())),
				AllowedScopes = TakeElements(DATA_ACCESS_SCOPE)
			}
		);
	}
}
