using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Kitias.IdentityServer.Configuration
{
	public static partial class Configuration
	{
		public static IEnumerable<IdentityResource> GetIdentityResources => TakeElements
		(
			new IdentityResources.OpenId(),
			new IdentityResources.Profile(),
			new IdentityResources.Email(),
			new IdentityResource(IDENTITY_CLAIMS_RESOURCE, TakeElements(JwtClaimTypes.Name, JwtClaimTypes.Role))
		);
	}
}
