using IdentityModel.Client;
using Kitias.Persistence.Entities.Default;
using Kitias.Providers.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;

namespace Kitias.API.Controllers
{
	/// <summary>
	/// Authorization endpoint
	/// </summary>
	public class AuthController : BaseController
	{
		private readonly IHttpClientFactory _clientFactory;
		private readonly IOptions<ISSecure> _secureOptions;

		/// <summary>
		/// Add services to controller
		/// </summary>
		/// <param name="logger">Logging</param>
		/// <param name="clientFactory">Client factory to create clients</param>
		public AuthController(ILogger<AuthController> logger, IHttpClientFactory clientFactory, IOptions<ISSecure> secureOptions) : base(logger) => (_clientFactory, _secureOptions)  = (clientFactory, secureOptions);

		/// <summary>
		/// Login user
		/// </summary>
		/// <param name="model">Model with user data</param>
		/// <returns>Status info</returns>
		[HttpPost("signIn")]
		[AllowAnonymous]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<string>> SignIn([FromBody] SignInRequestModel model)
		{
			var signInClient = _clientFactory.CreateClient();
			var discovery = await signInClient.GetDiscoveryDocumentAsync(_secureOptions.Value.Authority);
			var response = await signInClient.RequestPasswordTokenAsync(new()
			{
				Address = discovery.TokenEndpoint,
				ClientId = _secureOptions.Value.ClientId,
				ClientSecret = _secureOptions.Value.ClientSecret,
				UserName = model.UserName,
				Password = model.Password
			});

			if (response.IsError)
			{
				_logger.LogError("Couln't get data from identity-server");
				return BadRequest("");
			}
			//HttpContext.Response.Headers.Add("X-Content-Type-Options", "nosniff");
			//HttpContext.Response.Headers.Add("X-Xss-Protection", "1");
			//HttpContext.Response.Headers.Add("X-Frame-Options", "DENY");
			Response.Cookies.Append(
				".AspNetCore.Application.Guid",
				$"{response.AccessToken}",
				new()
				{
					HttpOnly = true
				}
			);
			return Ok(response.AccessToken);
		}
	}
}
