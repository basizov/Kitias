using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Kitias.API.Controllers
{
	/// <summary>
	/// Base controller
	/// </summary>
	[ApiController]
	[Route("api/[controller]")]
	[Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
	public class BaseController : ControllerBase
	{
		/// <summary>
		/// Logging
		/// </summary>
		protected readonly ILogger _logger;

		/// <summary>
		/// Add basic services
		/// </summary>
		/// <param name="logger">logging service</param>
		public BaseController(ILogger logger) => _logger = logger;
	}
}
