using Kitias.Persistence;
using Kitias.Persistence.Entities.Identity;
using Kitias.Providers.Interfaces;
using Kitias.Providers.Models;
using Kitias.Providers.Models.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Kitias.Providers.Implementations
{
	/// <summary>
	/// Main authorization provider
	/// </summary>
	public class AuthProvider : IAuthProvider
	{
		private readonly ILogger _logger;
		private readonly UserManager<User> _userManager;
		private readonly IdentityDataContext _dataContext;

		/// <summary>
		/// Constructor for take neccasary services
		/// </summary>
		/// <param name="logger">Logging</param>
		/// <param name="userManager">Manager for create and check user</param>
		/// <param name="dataContext">Manager for CRUD identity dbs</param>
		public AuthProvider(ILogger<AuthProvider> logger, UserManager<User> userManager, IdentityDataContext dataContext)
		{
			_logger = logger;
			_userManager = userManager;
			_dataContext = dataContext;
		}

		public async Task<Result<string>> SignUpAsync(SignUpRequestModel model)
		{
			if (await _userManager.Users.AnyAsync(u => u.Email == model.Email))
				return ReturnFailureResult<string>($"User with email: {model.Email} is existed");
			else if (await _userManager.Users.AnyAsync(u => u.UserName == model.UserName))
				return ReturnFailureResult<string>($"User with username: {model.UserName} is existed");
			var result = await _userManager.CreateAsync(new()
			{
				Email = model.Email,
				UserName = model.UserName
			}, model.Password);

			if (!result.Succeeded)
				return ReturnFailureResult<string>("Couldn't create user during registration");
			_logger.LogInformation($"User with email {model.Email} was successfully created");
			return ResultHandler.OnSuccess("User was successfully created");
		}

		public async Task<Result<string>> TokenSaveAsync(TokenRequestModel model)
		{
			var user = await _userManager
				.FindByNameAsync(model.UserName);

			if (user == null)
				return ReturnFailureResult<string>($"User with userName: {model.UserName} doesn't existed");
			_dataContext.UserTokens.Add(new()
			{
				Value = model.Token,
				Name = "refresh_token",
				UserId = user.Id
			});

			var	isSaved = await _dataContext.SaveChangesAsync();

			if (isSaved <= 0)
				return ReturnFailureResult<string>("Couldn't save token");
			return ResultHandler.OnSuccess("UserToken was successfully saved");
		}

		public async Task<Result<string>> TokenUpdateAsync(UpdateTokenRequestModel model)
		{
			var userToken = await _dataContext.UserTokens
				.Include(ut => ut.User)
				.SingleOrDefaultAsync(ut => ut.Value == model.OldToken);

			if (userToken == null)
				return ReturnFailureResult<string>($"UserToken with token: {model.OldToken} doesn't existed");
			userToken.Value = model.NewToken;
			userToken.Expires = DateTime.UtcNow.AddDays(7);
			_dataContext.UserTokens.Update(userToken);
			var	isSaved = await _dataContext.SaveChangesAsync();

			if (isSaved <= 0)
				return ReturnFailureResult<string>("Couldn't update token");
			return ResultHandler.OnSuccess("Token was successfully updated");
		}

		private Result<T> ReturnFailureResult<T>(string errorMessage)
			where T : class
		{
			_logger.LogError(errorMessage);
			return ResultHandler.OnFailure<T>(errorMessage);
		}
	}
}
