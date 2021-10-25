using IdentityModel.Client;
using IdentityServer4.AccessTokenValidation;
using Kitias.Identity.Server.Models.DTOs;
using Kitias.Identity.Server.Models.Entities;
using Kitias.Identity.Server.Models.RequestModels;
using Kitias.Identity.Server.Models.ResponseModels;
using Kitias.Identity.Server.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Kitias.Identity.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	[Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
	public class AuthController : ControllerBase
	{
		private readonly ILogger _logger;
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signinManager;
		private readonly DataContext _dataContext;
		private readonly IHttpClientFactory _clientFactory;

		public AuthController(ILogger<AuthController> logger, UserManager<User> userManager, SignInManager<User> signinManager, IHttpClientFactory clientFactory, DataContext dataContext)
		{
			_logger = logger;
			_userManager = userManager;
			_signinManager = signinManager;
			_clientFactory = clientFactory;
			_dataContext = dataContext;
		}

		[HttpPost("signUp")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> SignUp([FromBody] RegisterRequestModel model)
		{
			if (await _userManager.Users.AnyAsync(u => u.Email == model.Email))
				return BadRequestWithLogger($"User with email: {model.Email} is existed");
			else if (await _userManager.Users.AnyAsync(u => u.UserName == model.UserName))
				return BadRequestWithLogger($"User with username: {model.UserName} is existed");
			var result = await _userManager.CreateAsync(new()
			{
				Email = model.Email,
				UserName = model.UserName
			}, model.Password);

			if (!result.Succeeded)
				return BadRequestWithLogger("Couldn't create user during registration");
			_logger.LogInformation($"User with email {model.Email} was successfully created");
			return Ok("User was successfully created");
		}

		[HttpPost("signIn")]
		[AllowAnonymous]
		public async Task<IActionResult> SignIn([FromBody] SigninRequestModel model)
		{
			var user = await _userManager.FindByNameAsync(model.UserName);

			if (user == null)
				return UnauthorizedtWithLogger("Uncorrect user data");
			var isSignIn = await _signinManager
				.CheckPasswordSignInAsync(user, model.Password, false);

			if (!isSignIn.Succeeded)
				return UnauthorizedtWithLogger("Uncorrect user data");
			var client = _clientFactory.CreateClient();
			var disco = await client.GetDiscoveryDocumentAsync(@"https://localhost:44389");
			var tokenResponse = await client.RequestPasswordTokenAsync(new()
			{
				Address = disco.TokenEndpoint,
				ClientId = model.ClientId,
				ClientSecret = model.ClientSecret,
				UserName = model.UserName,
				Password = model.Password
			});

			if (tokenResponse.IsError)
				return UnauthorizedtWithLogger("Uncorrect user data");
			_logger.LogInformation($"Get tokens from identity server");
			var token = HttpContext.Request.Cookies[".AspNetCore.Application.Guid"];

			if (token == null)
			{
				var userToken = new UserToken
				{
					UserId = user.Id,
					Name = "refresh_token",
					Value = tokenResponse.RefreshToken,
					LoginProvider = disco.TokenEndpoint
				};

				_dataContext.UserTokens.Add(userToken);
				var isSaved = await _dataContext.SaveChangesAsync();

				if (isSaved <= 0)
					return BadRequestWithLogger("Couldn't create refresh token");
			}
			else
			{
				var userToken = await _dataContext.UserTokens
					.SingleOrDefaultAsync(t => t.Value == token);

				if (userToken == null)
					return BadRequestWithLogger("You couldn't get new token");
				userToken.Value = tokenResponse.RefreshToken;
				_dataContext.UserTokens.Update(userToken);
				var isSaved = await _dataContext.SaveChangesAsync();

				if (isSaved <= 0)
					return BadRequestWithLogger("Couldn't create refresh token");
			}
			var result = new UserDto
			{
				Email = user.Email,
				UserName = user.UserName,
				AccessToken = tokenResponse.AccessToken,
				RefreshToken = tokenResponse.RefreshToken
			};

			_logger.LogInformation($"User {user.Email} was successfully authenticeted");
			HttpContext.Response.Cookies.Append(
				".AspNetCore.Application.Guid",
				tokenResponse.RefreshToken,
				new()
				{
					MaxAge = TimeSpan.FromDays(7),
					Domain = ".localhost",
					Path = "/auth",
					HttpOnly = true
				}
			);
			return Ok(result);
		}

		[HttpPost("refresh")]
		[AllowAnonymous]
		public async Task<IActionResult> RefreshTokens(ClientCredentioalsRequest model)
		{
			var token = HttpContext.Request.Cookies[".AspNetCore.Application.Guid"];
			var userToken = await _dataContext.UserTokens
				.SingleOrDefaultAsync(t => t.Value == token);

			if (userToken == null)
				return BadRequestWithLogger("You couldn't get new token");
			var client = _clientFactory.CreateClient();
			var disco = await client.GetDiscoveryDocumentAsync(@"https://localhost:44389");
			var tokenResponse = await client.RequestRefreshTokenAsync(new()
			{
				Address = disco.TokenEndpoint,
				ClientId = model.ClientId,
				ClientSecret = model.ClientSecret,
				RefreshToken = token
			});

			if (tokenResponse.IsError)
				return BadRequestWithLogger("Uncorrect token");
			userToken.Value = tokenResponse.RefreshToken;
			_dataContext.UserTokens.Update(userToken);
			var isSaved = await _dataContext.SaveChangesAsync();

			if (isSaved <= 0)
				return BadRequestWithLogger("Couldn't update token");
			HttpContext.Response.Cookies.Append(
				".AspNetCore.Application.Guid",
				tokenResponse.RefreshToken,
				new()
				{
					MaxAge = TimeSpan.FromDays(7),
					Domain = ".localhost",
					Path = "/auth",
					HttpOnly = true
				}
			);
			return Ok(new TokenResponseModel
			{
				AccessToken = tokenResponse.AccessToken,
				RefreshToken = tokenResponse.RefreshToken
			});
		}

		[HttpPost("logout")]
		public async Task<IActionResult> Logout(ClientCredentioalsRequest model)
		{
			var token = HttpContext.Request.Cookies[".AspNetCore.Application.Guid"];
			var userToken = await _dataContext.UserTokens
				.SingleOrDefaultAsync(t => t.Value == token);

			if (userToken == null)
				return BadRequestWithLogger("You couldn't get new token");
			var client = _clientFactory.CreateClient();
			var disco = await client.GetDiscoveryDocumentAsync(@"https://localhost:44389");
			var tokenResponse = await client.RevokeTokenAsync(new()
			{
				Address = disco.RevocationEndpoint,
				ClientId = model.ClientId,
				ClientSecret = model.ClientSecret,
				Token = token,
				TokenTypeHint = "refresh_token"
			});

			if (tokenResponse.IsError)
				return BadRequestWithLogger("Uncorrect token");
			_dataContext.UserTokens.Remove(userToken);
			var isSaved = await _dataContext.SaveChangesAsync();

			if (isSaved <= 0)
				return BadRequestWithLogger("Couldn't revoke token");
			HttpContext.Response.Cookies.Append(".AspNetCore.Application.Guid", "", new()
			{
				Expires = DateTime.Now.AddDays(-1),
				Domain = ".localhost",
				Path = "/auth",
				HttpOnly = true
			});
			return Ok("Successfully logout");
		}

		private IActionResult BadRequestWithLogger(string message)
		{
			_logger.LogError(message);
			return BadRequest(message);
		}
		private IActionResult UnauthorizedtWithLogger(string message)
		{
			_logger.LogError(message);
			return Unauthorized(message);
		}
	}
}
