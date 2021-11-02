using Kitias.Persistence.DTOs;
using Kitias.Providers.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Kitias.API.Controllers
{
	/// <summary>
	/// Controller to work with groups
	/// </summary>
	public class GroupController : BaseController
	{
		private readonly IGroupProvider _groupProvider;

		/// <summary>
		/// Add services to the app
		/// </summary>
		/// <param name="logger">Logging</param>
		/// <param name="groupProvider">Provider to work with groups</param>
		public GroupController(ILogger<GroupController> logger, IGroupProvider groupProvider) : base(logger) => _groupProvider = groupProvider;

		/// <summary>
		/// Take all groups from db
		/// </summary>
		/// <returns>Groups</returns>
		[HttpGet]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public ActionResult<IEnumerable<GroupDto>> TakeGroups()
		{
			var result = _groupProvider.TakeGroups();

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}
	}
}
