using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Kitias.Identity.Server.OAuthOIDC.Configuration
{
	public static partial class Config
	{
		public static IEnumerable<ApiResource> GetApiResources => TakeElements
		(
			new ApiResource
			{
				Name = API_RESOURCE,
				DisplayName = "Kitias Web API",
				Description = "Allow the application to access Kitias Api on your behalf",
				Scopes = TakeElements(DATA_ACCESS_SCOPE),
				ApiSecrets = TakeElements(new Secret(KITIAS_API_SECRET.Sha256()))
			}
		);
	}
}
