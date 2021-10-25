using Kitias.API.Models;
using Kitias.Repository.Interfaces.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kitias.API.Controllers
{
	public class AuthController : BaseController
	{
		private readonly ILogger _logger;
		private readonly IHttpClientFactory _clientFactory;
		private readonly IUnitOfWork _unitOfWork;

		public AuthController(ILogger<AuthController> logger, IHttpClientFactory clientFactory, IUnitOfWork unitOfWork)
		{
			_logger = logger;
			_clientFactory = clientFactory;
			_unitOfWork = unitOfWork;
		}

		[HttpPost("register/student")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Register([FromBody] RegisterModel model)
		{
			var newPerson = _unitOfWork.Person.Create(new()
			{
				Name = model.Name,
				Surname = model.Surname,
				Patronymic = model.Patronymic,
				Email = model.Email
			});

			await _unitOfWork.SaveChangesAsync();
			_logger.LogInformation($"Student {model.Email} was created, by doesn't saved");
			var client = _clientFactory.CreateClient();
			HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeader);
			var tokenSplit = authHeader.ToString().Split(" ");

			if (tokenSplit.Length != 2)
				return UnauthorizedtWithLogger("Token is invalid");
			_logger.LogInformation("Access token is valid");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenSplit[0], tokenSplit[1]);
			var content = new StringContent(JsonSerializer.Serialize(new
			{
				model.Email,
				model.UserName,
				model.Password,
				model.RolesIds
			}), Encoding.UTF8, "application/json");
			var identityResponse = await client.PostAsync(@"https://localhost:44389/auth/register", content);

			if (identityResponse.StatusCode != HttpStatusCode.OK)
			{
				_unitOfWork.Person.Delete(newPerson);
				await _unitOfWork.SaveChangesAsync();
				return BadRequestWithLogger("Couldn't create user");
			}
			_logger.LogInformation($"User {model.Email} was successfully created");
			_logger.LogInformation($"Student {model.Email} was successfully created");
			return Ok("Student was successfully created");
		}

		[HttpPost("login")]
		[AllowAnonymous]
		public async Task<IActionResult> Login([FromBody] LoginModel model)
		{
			var client = _clientFactory.CreateClient();
			var content = new StringContent(JsonSerializer.Serialize(new
			{
				model.UserName,
				model.Password,
				ClientId = "Kitias.API",
				ClientSecret = "|||uqpySecret!"
			}), Encoding.UTF8, "application/json");
			var	identityResponse = await client.PostAsync(@"https://localhost:44389/auth/signin", content);
			var resultJson = await identityResponse.Content.ReadAsStringAsync();

			_logger.LogInformation($"USer {model.UserName} was successfully authorized");
			return Ok(JsonSerializer.Deserialize(resultJson, typeof(object)));
		}

		[HttpPost("refresh")]
		[AllowAnonymous]
		public async Task<IActionResult> RefreshToken()
		{
			var client = _clientFactory.CreateClient();
			var content = new StringContent(JsonSerializer.Serialize(new
			{
				ClientId = "Kitias.API",
				ClientSecret = "|||uqpySecret!"
			}), Encoding.UTF8, "application/json");
			var	identityResponse = await client.PostAsync(@"https://localhost:44389/auth/refresh", content);
			var resultJson = await identityResponse.Content.ReadAsStringAsync();

			return Ok(JsonSerializer.Deserialize(resultJson, typeof(object)));
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
