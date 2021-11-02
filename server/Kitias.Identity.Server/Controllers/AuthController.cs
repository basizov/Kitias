using Kitias.Providers.Interfaces;
using Kitias.Providers.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Kitias.Identity.Server.Controllers
{
	/// <summary>
	/// Controller to make authorization with db
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly IAuthProvider _authProvider;

		/// <summary>
		/// Constructor for authorization controller
		/// </summary>
		/// <param name="authProvider">Provider for working user with db</param>
		public AuthController(IAuthProvider authProvider) =>
			_authProvider = authProvider;

		/// <summary>
		/// Sign up method to register user
		/// </summary>
		/// <param name="model">Sign up model</param>
		/// <returns>Status string</returns>
		/// <response code="200">Success pesponse about user creation</response>
		/// <response code="400">Failure during registration a user</response>
		[HttpPost("signUp")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<string>> SignUpAsync(SignUpRequestModel model)
		{
			var result = await _authProvider.SignUpAsync(model);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}
	}
}
