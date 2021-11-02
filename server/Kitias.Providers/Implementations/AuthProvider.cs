using Kitias.Persistence.Entities.Identity;
using Kitias.Providers.Interfaces;
using Kitias.Providers.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Kitias.Providers.Implementations
{
	public class AuthProvider : IAuthProvider
	{
		private readonly ILogger _logger;
		private readonly UserManager<User> _userManager;

		public AuthProvider(ILogger<AuthProvider> logger, UserManager<User> userManager)
		{
			_logger = logger;
			_userManager = userManager;
		}

		public async Task<Result<string>> SignUpAsync(SignUpRequestModel model)
		{
			if (await _userManager.Users.AnyAsync(u => u.Email == model.Email))
			{
				var errorMessage = $"User with email: {model.Email} is existed";

				_logger.LogError(errorMessage);
				return ResultHandler.OnFailure<string>(errorMessage);
			}
			else if (await _userManager.Users.AnyAsync(u => u.UserName == model.UserName))
			{
				var errorMessage = $"User with username: {model.UserName} is existed";

				_logger.LogError(errorMessage);
				return ResultHandler.OnFailure<string>(errorMessage);
			}
			var result = await _userManager.CreateAsync(new()
			{
				Email = model.Email,
				UserName = model.UserName
			}, model.Password);

			if (!result.Succeeded)
			{
				var errorMessage = "Couldn't create user during registration";

				_logger.LogError(errorMessage);
				return ResultHandler.OnFailure<string>(errorMessage);
			}
			_logger.LogInformation($"User with email {model.Email} was successfully created");
			return ResultHandler.OnSuccess("User was successfully created");
		}
	}
}
