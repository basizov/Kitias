using Kitias.Persistence;
using Kitias.Persistence.Entities.Identity;
using Kitias.Providers.Interfaces;
using Kitias.Providers.Models;
using Kitias.Providers.Models.Request;
using Kitias.Providers.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Kitias.Providers.Implementations
{
	/// <summary>
	/// Main authorization provider
	/// </summary>
	public class AuthProvider : IAuthProvider
	{
		private readonly ILogger _logger;
		private readonly IConfiguration _config;
		private readonly UserManager<User> _userManager;
		private readonly IdentityDataContext _dataContext;
		private readonly EmailSender _emailSender;

		/// <summary>
		/// Constructor for take neccasary services
		/// </summary>
		/// <param name="logger">Logging</param>
		/// <param name="userManager">Manager for create and check user</param>
		/// <param name="dataContext">Manager for CRUD identity dbs</param>
		/// <param name="emailSender">Service to send emails</param>
		/// <param name="config">Config to get nessary values</param>
		public AuthProvider(ILogger<AuthProvider> logger, UserManager<User> userManager, IdentityDataContext dataContext, EmailSender emailSender, IConfiguration config)
		{
			_logger = logger;
			_userManager = userManager;
			_dataContext = dataContext;
			_emailSender = emailSender;
			_config = config;
		}

		public async Task<Result<string>> SignUpAsync(SignUpRequestModel model)
		{
			if (await _userManager.Users.AnyAsync(u => u.Email == model.Email))
				return ReturnFailureResult<string>($"User with email: {model.Email} is existed");
			else if (await _userManager.Users.AnyAsync(u => u.UserName == model.UserName))
				return ReturnFailureResult<string>($"User with username: {model.UserName} is existed");
			var user = new User
			{
				Email = model.Email,
				UserName = model.UserName
			};
			var result = await _userManager.CreateAsync(user, model.Password);

			if (!result.Succeeded)
				return ReturnFailureResult<string>("Couldn't create user during registration");
			_logger.LogInformation($"User with email {model.Email} was successfully created");
			await SendVerifyEmailToUserAsync(user);
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

		public async Task<Result<string>> LogoutAsync(string refreshToken)
		{
			var userToken = await _dataContext.UserTokens
				.SingleOrDefaultAsync(ut => ut.Value == refreshToken);

			if (userToken == null)
				return ReturnFailureResult<string>($"UserToken with token: {refreshToken} doesn't existed");
			_dataContext.UserTokens.Remove(userToken);
			var isSaved = await _dataContext.SaveChangesAsync();

			if (isSaved <= 0)
				return ReturnFailureResult<string>("Couldn't delete token");
			return ResultHandler.OnSuccess("Token was successfully deleted");
		}

		public async Task<Result<string>> SendVerifyEmailAsync(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);

			if (user == null)
				return ReturnFailureResult<string>($"Invalid user email: {email}");
			await SendVerifyEmailToUserAsync(user);
			return ResultHandler.OnSuccess($"Successfully send email");
		}

		public async Task<Result<string>> ConfirmVerifyEmailAsync(string token, string email)
		{
			var user = await _userManager.FindByEmailAsync(email);

			if (user == null)
				return ReturnFailureResult<string>($"Invalid user email: {email}");
			_logger.LogInformation($"Successfully get user by email: {email}");
			var decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(token));
			var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

			_logger.LogInformation("Successfully decode token");
			if (!result.Succeeded)
				return ReturnFailureResult<string>($"Invalid token of user: {email}");
			_logger.LogInformation("Successfully confirmation token");
			return ResultHandler.OnSuccess($"Successfully conformed email");
		}


		private async Task SendVerifyEmailToUserAsync(User user)
		{
			var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

			token = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));
			_logger.LogInformation($"Get verify token {token}");
			var verifyEmail = $"{_config.GetConnectionString("ServerDomain")}/auth/verifyEmail?token={token}&email={user.Email}";
			var emailMessage = $"<a href={verifyEmail}>Click to verify email please</a>";

			await _emailSender.SendEmailAsync(new()
			{
				To = user.Email,
				Subject = "Please verify an email",
				Message = emailMessage
			});
			_logger.LogInformation("Successfully sending confirmation email");
		}
		private Result<T> ReturnFailureResult<T>(string errorMessage)
			where T : class
		{
			_logger.LogError(errorMessage);
			return ResultHandler.OnFailure<T>(errorMessage);
		}
	}
}
