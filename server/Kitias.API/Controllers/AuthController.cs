using IdentityModel.Client;
using Kitias.Persistence.DTOs;
using Kitias.Persistence.Entities.Default;
using Kitias.Persistence.Enums;
using Kitias.Providers.Interfaces;
using Kitias.Providers.Models;
using Kitias.Providers.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kitias.API.Controllers
{
	/// <summary>
	/// Authorization endpoint
	/// </summary>
	public class AuthController : BaseController
	{
		private readonly IHttpClientFactory _clientFactory;
		private readonly IStudentProvider _studentProvider;
		private readonly ITeacherProvider _teacherProvider;
		private readonly IOptions<ISSecure> _secureOptions;
		private readonly IConfiguration _config;

		/// <summary>
		/// Add services to controller
		/// </summary>
		/// <param name="logger">Logging</param>
		/// <param name="clientFactory">Client factory to create clients</param>
		/// <param name="secureOptions">Config for identity server</param>
		/// <param name="config">Config to get domain</param>
		/// <param name="studentProvider">Provider to work with student db</param>
		/// <param name="teacherProvider">Provider to work with teacher dbparam>
		public AuthController(ILogger<AuthController> logger, IHttpClientFactory clientFactory, IOptions<ISSecure> secureOptions, IConfiguration config, IStudentProvider studentProvider, ITeacherProvider teacherProvider) : base(logger) => (_clientFactory, _secureOptions, _config, _studentProvider, _teacherProvider) = (clientFactory, secureOptions, config, studentProvider, teacherProvider);


		/// <summary>
		/// Flag tha user is auth
		/// </summary>
		/// <returns>Status message</returns>
		[HttpGet]
		[AllowAnonymous]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<string>> IsAuthAsync()
		{
			var token = Request.Cookies[".AspNetCore.Application.Guid"];

			if (token == null)
			{
				var identityDomain = _config.GetConnectionString("IdentityServerDomain");
				var (refreshClient, discovery) = await GetNewTokenAsync(identityDomain);

				if (!Request.Cookies.TryGetValue(".AspNetCore.Application.Guidance", out var refreshToken))
					return Unauthorized("Token doesn't existed");
				var (saveRefreshResponse, tokenResponse) = await ReloadTokenAsync(
					refreshClient,
					discovery,
					identityDomain,
					refreshToken
				);

				if (saveRefreshResponse.StatusCode != HttpStatusCode.OK)
				{
					_logger.LogError("Couln't update refresh token into identity-db");
					return BadRequest(await saveRefreshResponse.Content.ReadAsStringAsync());
				}
				SaveTokens(tokenResponse);
			}
			return Ok("User is auth");
		}

		/// <summary>
		/// Sign up endpoint for new user
		/// </summary>
		/// <param name="model">Model with user and student data</param>
		/// <returns>Status message</returns>
		[HttpPost("signUp")]
		//[Authorize(Roles = RolesNames.ADMIN_ROLE)]
		[AllowAnonymous]
		[Produces("application/json")]
		[ProducesResponseType(typeof(IEnumerable<StudentDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> SignUpAsync([FromBody] SignUpRegisterModel model)
		{
			object returnResult;

			if (model.PersonType == RolesNames.STUDENT_ROLE)
			{
				var result = await _studentProvider.CreateStudentAsync(new()
				{
					Email = model.Email,
					GroupNumber = model.GroupNumber,
					Name = model.Name,
					Patronymic = model.Patronymic,
					Surname = model.Surname
				});

				if (!result.IsSuccess)
					return BadRequest(result.Error);
				returnResult = result.Value;
			}
			else if (model.PersonType == RolesNames.TEACHER_ROLE)
			{
				var result = await _teacherProvider.CreateTeacherAsync(new()
				{
					Email = model.Email,
					Name = model.Name,
					Patronymic = model.Patronymic,
					Surname = model.Surname
				});

				if (!result.IsSuccess)
					return BadRequest(result.Error);
				returnResult = result.Value;
			}
			else
			{
				_logger.LogError($"Couldn't find the same role {model.PersonType}");
				return BadRequest($"Invalid user type {model.PersonType}");
			}
			var signUpClient = _clientFactory.CreateClient();
			var identityDomain = _config.GetConnectionString("IdentityServerDomain");
			var token = Request.Cookies[".AspNetCore.Application.Guid"];
			signUpClient.SetBearerToken(token);
			var signUpResponse = await signUpClient.PostAsync($"{identityDomain}/auth/signUp", new StringContent
			(
				JsonSerializer.Serialize(new SignUpRequestModel
				{
					UserName = model.UserName,
					Email = model.Email,
					Password = model.Password,
					Roles = model.Roles
				}),
				Encoding.UTF8,
				"application/json"
			));

			if (signUpResponse.StatusCode != HttpStatusCode.OK)
			{
				_logger.LogError("Couln't sign up in identity-db");
				return BadRequest(await signUpResponse.Content.ReadAsStringAsync());
			}
			return Ok(returnResult);
		}

		/// <summary>
		/// Sign in endpoint for user
		/// </summary>
		/// <param name="model">Model with user data</param>
		/// <returns>Status message</returns>
		[HttpPost("signIn")]
		[AllowAnonymous]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<string>> SignInAsync([FromBody] SignInRequestModel model)
		{
			var signInClient = _clientFactory.CreateClient();
			var discovery = await signInClient.GetDiscoveryDocumentAsync(_secureOptions.Value.Authority);
			var tokenResponse = await signInClient.RequestPasswordTokenAsync(new()
			{
				Address = discovery.TokenEndpoint,
				ClientId = _secureOptions.Value.ClientId,
				ClientSecret = _secureOptions.Value.ClientSecret,
				UserName = model.UserName,
				Password = model.Password
			});

			if (tokenResponse.IsError)
			{
				_logger.LogError("Couln't get data from identity-server");
				throw new HttpRequestException(tokenResponse.ErrorDescription);
			}
			var identityDomain = _config.GetConnectionString("IdentityServerDomain");
			var saveRefreshResponse = await signInClient.PostAsync($"{identityDomain}/auth/token", new StringContent
			(
				JsonSerializer.Serialize(new TokenRequestModel
				{
					UserName = model.UserName,
					Token = tokenResponse.RefreshToken
				}),
				Encoding.UTF8,
				"application/json"
			));

			if (saveRefreshResponse.StatusCode != HttpStatusCode.OK)
			{
				_logger.LogError("Couln't save refresh token to identity-db");
				return BadRequest(await saveRefreshResponse.Content.ReadAsStringAsync());
			}
			SaveTokens(tokenResponse);
			return Ok("User was successfully logged in");
		}

		/// <summary>
		/// Update old refresh token
		/// </summary>
		/// <returns>New access token</returns>
		[HttpPost("token")]
		[AllowAnonymous]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<string>> TakeNewTokenAsync()
		{
			var identityDomain = _config.GetConnectionString("IdentityServerDomain");
			var (refreshClient, discovery) = await GetNewTokenAsync(identityDomain);

			if (!Request.Cookies.TryGetValue(".AspNetCore.Application.Guidance", out var refreshToken))
				return Unauthorized("Token doesn't existed");
			var (saveRefreshResponse, tokenResponse) = await ReloadTokenAsync(
				refreshClient,
				discovery,
				identityDomain,
				refreshToken
			);

			if (saveRefreshResponse.StatusCode != HttpStatusCode.OK)
			{
				_logger.LogError("Couln't update refresh token into identity-db");
				return BadRequest(await saveRefreshResponse.Content.ReadAsStringAsync());
			}
			SaveTokens(tokenResponse);
			return Ok("User was successfully reconnected");
		}

		private async Task<(HttpClient, DiscoveryDocumentResponse)> GetNewTokenAsync(string identityDomain)
		{
			var refreshClient = _clientFactory.CreateClient();
			var discovery = await refreshClient.GetDiscoveryDocumentAsync(_secureOptions.Value.Authority);

			return (refreshClient, discovery);
		}

		private async Task<(HttpResponseMessage, TokenResponse)> ReloadTokenAsync(
			HttpClient refreshClient,
			DiscoveryDocumentResponse discovery,
			string identityDomain,
			string oldToken
		)
		{
			var tokenResponse = await refreshClient.RequestRefreshTokenAsync(new()
			{
				Address = discovery.TokenEndpoint,
				ClientId = _secureOptions.Value.ClientId,
				ClientSecret = _secureOptions.Value.ClientSecret,
				RefreshToken = oldToken
			});

			if (tokenResponse.IsError)
			{
				_logger.LogError("Couln't get new tokens from identity-server");
				throw new HttpRequestException(tokenResponse.ErrorDescription);
			}
			var saveRefreshResponse = await refreshClient.PutAsync($"{identityDomain}/auth/token", new StringContent
			(
				JsonSerializer.Serialize(new UpdateTokenRequestModel
				{
					OldToken = oldToken,
					NewToken = tokenResponse.RefreshToken
				}),
				Encoding.UTF8,
				"application/json"
			));

			return (saveRefreshResponse, tokenResponse);
		}

		private void SaveTokens(TokenResponse tokenResponse)
		{
			Response.Cookies.Append(
				".AspNetCore.Application.Guid",
				$"{tokenResponse.AccessToken}",
				new()
				{
					HttpOnly = true,
					Path = "/api",
					Expires = DateTime.UtcNow.AddSeconds(10),
					MaxAge = TimeSpan.FromHours(1)
				}
			);
			Response.Cookies.Append(
				".AspNetCore.Application.Guidance",
				$"{tokenResponse.RefreshToken}",
				new()
				{
					HttpOnly = true,
					Path = "/api/Auth",
					Expires = DateTime.UtcNow.AddDays(7),
					MaxAge = TimeSpan.FromDays(7)
				}
			);
		}

		/// <summary>
		/// Logout user from system
		/// </summary>
		/// <returns>Status message</returns>
		[HttpGet("logout")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<string>> LogoutAsync()
		{
			if (!Request.Cookies.TryGetValue(".AspNetCore.Application.Guidance", out var refreshToken))
				return Unauthorized("Token doesn't existed");
			var identityDomain = _config.GetConnectionString("IdentityServerDomain");
			var logoutClient = _clientFactory.CreateClient();
			var newRefreshResponse = await logoutClient.PostAsync($"{identityDomain}/auth/logout", new StringContent
			(
				JsonSerializer.Serialize(refreshToken),
				Encoding.UTF8,
				"application/json"
			));

			if (newRefreshResponse.StatusCode != HttpStatusCode.OK)
			{
				_logger.LogError("Couln't take old refreshToken");
				return Unauthorized("Please enter to the app");
			}
			Response.Cookies.Append(
				".AspNetCore.Application.Guid",
				"",
				new()
				{
					HttpOnly = true,
					Path = "/api",
					Expires = DateTime.UtcNow.AddDays(-1)
				}
			);
			Response.Cookies.Append(
				".AspNetCore.Application.Guidance",
				"",
				new()
				{
					HttpOnly = true,
					Path = "/api/Auth",
					Expires = DateTime.UtcNow.AddDays(-1)
				}
			);
			return Ok("Successfully logout");
		}
	}
}
