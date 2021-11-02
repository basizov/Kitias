using Kitias.API.Models;
using Kitias.Repository.Interfaces.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Kitias.API.Controllers
{
	public class AuthController : BaseController
	{
		private readonly ILogger _logger;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHttpClientFactory _clientFactory;

		public AuthController(ILogger<AuthController> logger, IUnitOfWork unitOfWork, IHttpClientFactory clientFactory)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
			_clientFactory = clientFactory;
		}

		[HttpPost("signUp")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> SignUp([FromBody] RegisterModel model)
		{
			var newPerson = _unitOfWork.Person.Create(new()
			{
				Name = model.Name,
				Surname = model.Surname,
				Patronymic = model.Patronymic,
				Email = model.Email
			});

			try
			{
				await _unitOfWork.SaveChangesAsync();
				_logger.LogInformation($"Student {model.Email} was created, but doesn't saved");
				await TakeIdentityResponseAsync<string>(new
				{
					model.Email,
					model.UserName,
					model.Password,
					model.RolesIds
				}, @"https://localhost:44389/auth/signUp");
				_logger.LogInformation($"User {model.Email} was successfully created");
				_logger.LogInformation($"Student {model.Email} was successfully created");
				return Ok("Student was successfully created");
			}
			catch (Exception ex)
			{
				_unitOfWork.Person.Delete(newPerson);
				await _unitOfWork.SaveChangesAsync();
				_logger.LogError(ex, ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("signIn")]
		[AllowAnonymous]
		public async Task<IActionResult> SignIn([FromBody] LoginModel model)
		{
			try
			{
				var	result = await TakeIdentityResponseAsync<SignInResponse>(new
				{
					model.UserName,
					model.Password,
					ClientId = "Kitias.API",
					ClientSecret = "|||uqpySecret!"
				}, @"https://localhost:44389/auth/signIn");

				SetAccessTokenToCookies(result.AccessToken, 1);
				_logger.LogInformation($"User {model.UserName} was successfully authorized");
				return Ok("User is successfully authorized");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("refresh")]
		[AllowAnonymous]
		public async Task<IActionResult> RefreshTokens()
		{
			try
			{
				var result = await TakeIdentityResponseAsync<TokenResponse>(new
				{
					ClientId = "Kitias.API",
					ClientSecret = "|||uqpySecret!"
				}, @"https://localhost:44389/auth/refresh");

				SetAccessTokenToCookies(result.AccessToken, 1);
				_logger.LogInformation($"Take a new tokens pair {result}");
				return Ok("Create new tokens");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("logout")]
		public async Task<IActionResult> Logout()
		{
			try
			{
				await TakeIdentityResponseAsync<string>(new
				{
					ClientId = "Kitias.API",
					ClientSecret = "|||uqpySecret!"
				}, @"https://localhost:44389/auth/logout");

				_logger.LogInformation($"User was successfully logout");
				SetAccessTokenToCookies("", -1);
				return Ok("Successfully logout");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return BadRequest(ex.Message);
			}
		}

		private async Task<T> TakeIdentityResponseAsync<T>(object data, string url)
			where T : class
		{
			var client = _clientFactory.CreateClient();
			_logger.LogInformation($"Create a httpClient = {client}");
			(var sheme, var token) = TakeTokenFromHeader();

			if (!string.IsNullOrEmpty(sheme) && !string.IsNullOrEmpty(token))
			{
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(sheme, token);
				_logger.LogInformation($"Create authorization header {sheme} {token}");
			}
			var content = new StringContent(
				JsonConvert.SerializeObject(data),
				Encoding.UTF8,
				"application/json"
			);
			_logger.LogInformation($"Create a request content {content}");
			var identityResponse = await client.PostAsync(url, content);
			var jsonData = await identityResponse.Content.ReadAsStringAsync();

			_logger.LogInformation($"Take identity response {jsonData}");
			if (identityResponse.StatusCode != HttpStatusCode.OK)
				throw new BadHttpRequestException(jsonData);
			else if (jsonData is T)
				return jsonData as T;
			return JsonConvert.DeserializeObject<T>(jsonData);
		}
		private (string, string) TakeTokenFromHeader()
		{
			HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeader);
			_logger.LogInformation($"Take authorization header ${authHeader}");
			var tokenSplit = authHeader.ToString().Split(" ");

			if (tokenSplit.Length != 2)
				return ("", "");
			_logger.LogInformation($"Took authorization sheme ${tokenSplit[0]} and token {tokenSplit[1]}");
			return (tokenSplit[0], tokenSplit[1]);
		}
		private void SetAccessTokenToCookies(string token, int days)
		{
			HttpContext.Response.Cookies.Append(
				".AspNetCore.Application.Guid",
				token,
				new()
				{
					MaxAge = TimeSpan.FromHours(days),
					Domain = ".localhost",
					Path = "/api",
					HttpOnly = true
				}
			);
		}
	}
}
