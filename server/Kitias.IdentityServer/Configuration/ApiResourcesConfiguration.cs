using IdentityModel;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace Kitias.IdentityServer.Configuration
{
	public static partial class Configuration
	{
		public static IEnumerable<ApiResource> GetApiResources => TakeElements
		(
			new ApiResource
			{
				Name = API_RESOURCE,
				DisplayName = "Kitias Web API",
				Description = "Allow the application to access Kitias Api on your behalf",
				Scopes = new[] { DATA_ACCESS_SCOPE },
				UserClaims = TakeElements(JwtClaimTypes.Name, JwtClaimTypes.Role)
			}
		);
	}
}
