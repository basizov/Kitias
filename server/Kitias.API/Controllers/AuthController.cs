using Kitias.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kitias.API.Controllers
{
	public class AuthController : BaseController
	{
		private readonly IHttpClientFactory _clientFactory;

		public AuthController(IHttpClientFactory clientFactory) => _clientFactory = clientFactory;

		[HttpPost("register/student")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Register([FromBody] RegisterModel model)
		{
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

			return Ok(JsonSerializer.Deserialize(resultJson, typeof(object)));
		}
	}
}
