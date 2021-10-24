using Kitias.Providers.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kitias.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class BaseController : ControllerBase
	{
		public IActionResult HandleResult<T>(Result<T> result)
			where T : class
		{
			if (result == null)
				return NotFound();
			else if (result.IsSuccess && result.Value != null)
				return Ok(result.Value);
			else if (result.IsSuccess && result.Value == null)
				return NotFound();
			return BadRequest(result.Error);
		}
	}
}
