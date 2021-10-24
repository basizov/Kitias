using IdentityModel.Client;
using IdentityServer4.AccessTokenValidation;
using Kitias.Identity.Server.Models;
using Kitias.Identity.Server.Models.DTOs;
using Kitias.Identity.Server.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
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
		private readonly IHttpClientFactory _clientFactory;

		public AuthController(ILogger<AuthController> logger, UserManager<User> userManager, SignInManager<User> signinManager, IHttpClientFactory clientFactory)
		{
			_logger = logger;
			_userManager = userManager;
			_signinManager = signinManager;
			_clientFactory = clientFactory;
		}

		[HttpPost("register")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> RegisterUser([FromBody] RegisterModel model)
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

		[HttpPost("signin")]
		[AllowAnonymous]
		public async Task<IActionResult> Signin([FromBody] SigninModel model)
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
			var accessToken = tokenResponse.AccessToken;
			var refreshToken = tokenResponse.RefreshToken;
			var result = new UserDto
			{
				Email = user.Email,
				UserName = user.UserName,
				AccessToken = accessToken,
				RefreshToken = refreshToken
			};

			_logger.LogInformation($"User {user.Email} was successfully authenticeted");
			return Ok(result);
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
