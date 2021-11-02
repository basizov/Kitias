using IdentityServer4.Models;
using System.Collections.Generic;

namespace Kitias.Identity.Server.Config
{
	public static partial class OAuthOIDCConfig
	{
		public static IEnumerable<ApiResource> TakeAPIResources= new List<ApiResource>
		{
			new()
			{
				Name = KITIAS_RESOURCE,
				DisplayName = "Kitias",
				Description = "Allow the application to access Kitias on your behalf",
				ApiSecrets = new List<Secret>
				{
					new(KITIAS_API_SECRET.Sha256())
				}
			}
		};
	}
}
