using Kitias.Persistence;
using Kitias.Persistence.Entities.Identity;
using Kitias.Persistence.Enums;
using Kitias.Providers.Interfaces;
using Kitias.Providers.Models;
using Kitias.Providers.Models.Request;
using Kitias.Providers.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
		private readonly RoleManager<Role> _roleManager;
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
		/// <param name="roleManager">Manager to work with roles</param>
		public AuthProvider(ILogger<AuthProvider> logger, UserManager<User> userManager, IdentityDataContext dataContext, EmailSender emailSender, IConfiguration config, RoleManager<Role> roleManager) => (_logger, _userManager, _dataContext, _emailSender, _config, _roleManager) = (logger, userManager, dataContext, emailSender, config, roleManager);

		public async Task<Result<string>> SignUpAsync(SignUpRequestModel model)
		{
			if (await _userManager.Users.AnyAsync(u => u.Email == model.Email))
				return ReturnFailureResult<string>($"User with email: {model.Email} is existed");
			else if (await _userManager.Users.AnyAsync(u => u.UserName == model.UserName))
				return ReturnFailureResult<string>($"User with username: {model.UserName} is existed");
			else if (model.Roles == null)
				return ReturnFailureResult<string>("Please indicate roles");
			var user = new User
			{
				Email = model.Email,
				UserName = model.UserName
			};
			var result = await _userManager.CreateAsync(user, model.Password);

			if (!result.Succeeded)
				return ReturnFailureResult<string>("Couldn't create user during registration");
			_logger.LogInformation($"User with email {model.Email} was successfully created");
			foreach (var role in model.Roles)
			{
				var roleEntity = await _dataContext.Roles
					.SingleOrDefaultAsync(r => r.Name == role);

				if (roleEntity == null)
					return ReturnFailureResult<string>("There aren't this role");
				_dataContext.UserRoles.Add(new()
				{
					UserId = user.Id,
					RoleId = roleEntity.Id
				});
				_logger.LogInformation($"Add new role {role} to user {user.Email}");
			}
			var isSaved = await _dataContext.SaveChangesAsync();

			if (isSaved <= 0)
				throw new ApplicationException("Couldn't create user role entities");
			// TODO: Uncommitted after sign up into SendGrid
			//await SendVerifyEmailToUserAsync(user);
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
			var isSaved = await _dataContext.SaveChangesAsync();

			if (isSaved <= 0)
				throw new ApplicationException("Couldn't save token");
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
			var isSaved = await _dataContext.SaveChangesAsync();

			if (isSaved <= 0)
				throw new ApplicationException("Couldn't update token");
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
				throw new ApplicationException("Couldn't delete token");
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
				throw new ApplicationException($"Invalid token of user: {email}");
			_logger.LogInformation("Successfully confirmation token");
			return ResultHandler.OnSuccess($"Successfully conformed email");
		}

		public async Task<Result<string>> RegisterNewRolesAsync(IEnumerable<string> roles)
		{
			try
			{
				foreach (var role in roles)
				{
					var checkedRole = Helpers.GetEnumMemberFromString<RoleTypes>(role);

					if (await _roleManager.Roles.AnyAsync(r => r.Name == role))
						return ReturnFailureResult<string>($"Role {role} is existed");
					await _roleManager.CreateAsync(new()
					{
						Name = role,
						NormalizedName = checkedRole.ToString()
					});
					_logger.LogInformation($"Role {role} wass successfully added");
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return ReturnFailureResult<string>("Rundim role was defined");
			}
			return ResultHandler.OnSuccess($"Roles was successfully added");
		}

		public async Task<Result<string>> AddRolesToUserAsync(RolesToUserRequestModel model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);

			if (user == null)
				return ReturnFailureResult<string>($"Invalid user email: {model.Email}");
			foreach (var role in model.Roles)
			{
				var roleEntity = await _dataContext.Roles
					.SingleOrDefaultAsync(r => r.Name == role);

				if (roleEntity == null)
					return ReturnFailureResult<string>("There aren't this role");
				var userRoleEntity = await _dataContext.UserRoles
					.SingleOrDefaultAsync(r => r.RoleId == roleEntity.Id && r.UserId == user.Id);

				if (userRoleEntity != null)
					return ReturnFailureResult<string>($"{user.Email} have {role} role");
				_dataContext.UserRoles.Add(new()
				{
					UserId = user.Id,
					RoleId = roleEntity.Id
				});
				var isSaved = await _dataContext.SaveChangesAsync();

				if (isSaved <= 0)
					throw new ApplicationException($"Couldn't add new role to {model.Email}");
				_logger.LogInformation($"Add new role {role} to user {user.Email}");
			}
			return ResultHandler.OnSuccess($"Add roles to {model.Email}");
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
