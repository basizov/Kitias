using IdentityServer4.Models;
using System.Collections.Generic;

namespace Kitias.IdentityServer.Configuration
{
	public static partial class Configuration
	{
		public static IEnumerable<ApiScope> GetApiScopes => TakeElements
		(
			new ApiScope(DATA_ACCESS_SCOPE, "Get access to data from Kitias")
		);
	}
}
