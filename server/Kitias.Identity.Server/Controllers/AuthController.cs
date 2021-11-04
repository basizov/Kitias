using Kitias.Providers.Interfaces;
using Kitias.Providers.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
		public AuthController(IAuthProvider authProvider) => _authProvider = authProvider;

		/// <summary>
		/// Sign up method to register user
		/// </summary>
		/// <param name="model">Sign up model</param>
		/// <returns>Status string</returns>
		/// <response code="200">Success pesponse about user creation</response>
		/// <response code="400">Failure during registration a user</response>
		[HttpPost("signUp")]
		[AllowAnonymous]
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

		/// <summary>
		/// Save refresh token method
		/// </summary>
		/// <param name="model">Model to save token for user</param>
		/// <returns>Status message</returns>
		[HttpPost("token")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<string>> SaveTokenAsync(TokenRequestModel model)
		{
			var result = await _authProvider.TokenSaveAsync(model);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			Response.Cookies.Append(
				".AspNetCore.Application.Guid",
				$"{model.Token}",
				new()
				{
					HttpOnly = true,
					Path = "/auth",
					Expires = DateTime.UtcNow.AddDays(7),
					MaxAge = TimeSpan.FromDays(7)
				}
			);
			return Ok(result.Value);
		}

		/// <summary>
		/// Update refresh token controller
		/// </summary>
		/// <param name="model">Model to update token</param>
		/// <returns>Status message</returns>
		[HttpPut("token")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<string>> UpdateTokenAsync([FromBody] UpdateTokenRequestModel model)
		{
			var result = await _authProvider.TokenUpdateAsync(model);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			Response.Cookies.Append(
				".AspNetCore.Application.Guid",
				$"{model.NewToken}",
				new()
				{
					HttpOnly = true,
					Path = "/auth",
					Expires = DateTime.UtcNow.AddDays(7),
					MaxAge = TimeSpan.FromDays(7)
				}
			);
			return Ok(result.Value);
		}

		/// <summary>
		/// Take refresh token from cookies
		/// </summary>
		/// <returns>Refresh token</returns>
		[HttpGet("token")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public ActionResult<string> TakeToken()
		{
			if (!Request.Cookies.TryGetValue(".AspNetCore.Application.Guid", out var refreshToken))
				return BadRequest("Token doesn't existed");
			return Ok(refreshToken);
		}

		/// <summary>
		/// Logout from accout
		/// </summary>
		/// <returns>Status message</returns>
		[HttpGet("logout")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public ActionResult<string> Logout()
		{
			if (!Request.Cookies.TryGetValue(".AspNetCore.Application.Guid", out var refreshToken))
				return BadRequest("Token doesn't existed");
			Response.Cookies.Append(
				".AspNetCore.Application.Guid",
				"",
				new()
				{
					HttpOnly = true,
					Path = "/auth",
					Expires = DateTime.UtcNow.AddDays(-1)
				}
			);
			return Ok("User was successfully logout");
		}

		/// <summary>
		/// Verify email from token
		/// </summary>
		/// <param name="token">Token to verify email</param>
		/// <param name="email">Receiver email</param>
		/// <returns>Status message</returns>
		[HttpGet("verifyEmail")]
		[AllowAnonymous]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<string>> ConfirmVerifyEmailAsync([FromQuery] string token, [FromQuery] string email)
		{
			var result = await _authProvider.ConfirmVerifyEmailAsync(token, email);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Resendv verify token email
		/// </summary>
		/// <param name="email">Receiver email</param>
		/// <returns>Status message</returns>
		[HttpGet("verifyEmail/resend")]
		[AllowAnonymous]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<string>> ResendVerifyEmailAsync([FromQuery] string email)
		{
			var result = await _authProvider.SendVerifyEmailAsync(email);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}
	}
}
