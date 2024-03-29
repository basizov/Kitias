﻿using Kitias.Providers.Interfaces;
using Kitias.Providers.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

		/// <summary>
		/// Register new roles method
		/// </summary>
		/// <param name="roles">New roles</param>
		/// <returns>Status string</returns>
		[HttpPost("roles")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<string>> RegisterNewRolesAsync(IEnumerable<string> roles)
		{
			var result = await _authProvider.RegisterNewRolesAsync(roles);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Add new roles to user method
		/// </summary>
		/// <param name="model">Model with roles and user email</param>
		/// <returns>Status string</returns>
		[HttpPost("user/roles")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<string>> AddRolesToUserAsync(RolesToUserRequestModel model)
		{
			var result = await _authProvider.AddRolesToUserAsync(model);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Save refresh token method
		/// </summary>
		/// <param name="model">Model to save token for user</param>
		/// <returns>Roles</returns>
		[HttpPost("token")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<IEnumerable<string>>> SaveTokenAsync(TokenRequestModel model)
		{
			var result = await _authProvider.TokenSaveAsync(model);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
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
			return Ok(result.Value);
		}

		/// <summary>
		/// Logout from accout
		/// </summary>
		/// <returns>Status message</returns>
		[HttpPost("logout")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<string>> LogoutAsync([FromBody] string token)
		{
			var result = await _authProvider.LogoutAsync(token);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
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
