﻿using IdentityServer4.Models;
using System.Collections.Generic;

namespace Kitias.Identity.Server.Config
{
	public static partial class OAuthOIDCConfig
	{
		public static IEnumerable<Client> TakeClients = new List<Client>
		{
			new()
			{
				ClientId = KITIAS_API_ID,
				ClientName = KITIAS_API_NAME,
				ClientSecrets = new List<Secret>
				{
					new(KITIAS_API_SECRET.Sha256())
				},
				AllowedGrantTypes = GrantTypes.ResourceOwnerPassword
			}
		};
	}
}
