using Kitias.Persistence.Entities.Identity;
using Kitias.Providers.Models;
using Kitias.Providers.Models.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kitias.Providers.Interfaces
{
	/// <summary>
	/// USer authorization methods
	/// </summary>
	public interface IAuthProvider
	{
		/// <summary>
		/// Register new user
		/// </summary>
		/// <param name="model">Register model</param>
		/// <returns>Statuse message</returns>
		Task<Result<string>> SignUpAsync(SignUpRequestModel model);
		/// <summary>
		/// Save refresh token to db
		/// </summary>
		/// <param name="model">Token save model</param>
		/// <returns>Status message</returns>
		Task<Result<string>> TokenSaveAsync(TokenRequestModel model);
		/// <summary>
		/// Update refresh token
		/// </summary>
		/// <param name="model">Model to update token</param>
		/// <returns>Status message</returns>
		Task<Result<string>> TokenUpdateAsync(UpdateTokenRequestModel model);
		/// <summary>
		/// Delete refresh token from db
		/// </summary>
		/// <param name="refreshToken">Token to delete UserToken</param>
		/// <returns>Status message</returns>
		Task<Result<string>> LogoutAsync(string refreshToken);
		/// <summary>
		/// Send verify email method
		/// </summary>
		/// <param name="email">User email</param>
		/// <returns>Status message</returns>
		Task<Result<string>> SendVerifyEmailAsync(string email);
		/// <summary>
		/// Confirm verify email method
		/// </summary>
		/// <param name="token">Email confirmation token</param>
		/// <param name="email">User email</param>
		/// <returns>Status message</returns>
		Task<Result<string>> ConfirmVerifyEmailAsync(string token, string email);
		/// <summary>
		/// Register new roles to db
		/// </summary>
		/// <param name="roles">New roles</param>
		/// <returns>Status message</returns>
		Task<Result<string>> RegisterNewRolesAsync(IEnumerable<string> roles);
		/// <summary>
		/// Add new roles to user
		/// </summary>
		/// <param name="model">Model to create new UserRole</param>
		/// <returns>Status message</returns>
		Task<Result<string>> AddRolesToUserAsync(RolesToUserRequestModel model);
	}
}
