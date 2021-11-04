using IdentityModel.Client;
using Kitias.Persistence.Entities.Default;
using Kitias.Providers.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
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
		private readonly IOptions<ISSecure> _secureOptions;
		private readonly IConfiguration _config;

		/// <summary>
		/// Add services to controller
		/// </summary>
		/// <param name="logger">Logging</param>
		/// <param name="clientFactory">Client factory to create clients</param>
		/// <param name="secureOptions">Config for identity server</param>
		/// <param name="config">Config to get domain</param>
		public AuthController(ILogger<AuthController> logger, IHttpClientFactory clientFactory, IOptions<ISSecure> secureOptions, IConfiguration config) : base(logger) => (_clientFactory, _secureOptions, _config) = (clientFactory, secureOptions, config);

		/// <summary>
		/// Sign in endpoint for user
		/// </summary>
		/// <param name="model">Model with user data</param>
		/// <returns>Access token</returns>
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
			Response.Cookies.Append(
				".AspNetCore.Application.Guid",
				$"{tokenResponse.AccessToken}",
				new()
				{
					HttpOnly = true,
					Path = "/api",
					Expires = DateTime.UtcNow.AddHours(1),
					MaxAge = TimeSpan.FromHours(1)
				}
			);
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
			var refreshClient = _clientFactory.CreateClient();
			var discovery = await refreshClient.GetDiscoveryDocumentAsync(_secureOptions.Value.Authority);
			var newRefreshResponse = await refreshClient.GetAsync($"{identityDomain}/auth/token");

			if (newRefreshResponse.StatusCode != HttpStatusCode.OK)
			{
				_logger.LogError("Couln't take old refreshToken");
				return Unauthorized("Please enter to the app");
			}
			var oldToken = await newRefreshResponse.Content.ReadAsStringAsync();
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

			if (saveRefreshResponse.StatusCode != HttpStatusCode.OK)
			{
				_logger.LogError("Couln't update refresh token into identity-db");
				return BadRequest(await saveRefreshResponse.Content.ReadAsStringAsync());
			}
			Response.Cookies.Append(
				".AspNetCore.Application.Guid",
				$"{tokenResponse.AccessToken}",
				new()
				{
					HttpOnly = true,
					Path = "/api",
					Expires = DateTime.UtcNow.AddHours(1),
					MaxAge = TimeSpan.FromHours(1)
				}
			);
			return Ok("User was successfully reconnected");
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
			var identityDomain = _config.GetConnectionString("IdentityServerDomain");
			var logoutClient = _clientFactory.CreateClient();
			var newRefreshResponse = await logoutClient.GetAsync($"{identityDomain}/auth/logout");

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
			return Ok("Successfully logout");
		}
	}
}
