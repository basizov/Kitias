using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Kitias.Identity.Server.Config
{
	public static partial class OAuthOIDCConfig
	{
		/// <summary>
		/// Initial api scopes
		/// </summary>
		public static IEnumerable<ApiScope> TakeScopes = new List<ApiScope>
		{
			new()
			{
				Name = "kitias.data",
				Description = "Get access to data from Kitias",
				DisplayName = "Kitias Web Api Data",
				UserClaims = new List<string>
				{
					JwtClaimTypes.Email,
					JwtClaimTypes.Role,
					JwtClaimTypes.Name
				}
			}
		};
	}
}
