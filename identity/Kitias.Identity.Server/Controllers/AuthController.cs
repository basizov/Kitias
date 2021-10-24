using IdentityServer4.AccessTokenValidation;
using Kitias.Identity.Server.Models;
using Kitias.Identity.Server.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

		public AuthController(ILogger<AuthController> logger, UserManager<User> userManager)
		{
			_logger = logger;
			_userManager = userManager;
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

		//public async Task<IActionResult> CheckAndSetRefreshToken()
		//{

		//}

		private IActionResult BadRequestWithLogger(string message)
		{
			_logger.LogError(message);
			return BadRequest(message);
		}
	}
}
