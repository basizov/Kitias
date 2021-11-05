using Kitias.Persistence.DTOs;
using Kitias.Providers.Interfaces;
using Kitias.Providers.Models.Group;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

		/// <summary>
		/// Take group from db by id
		/// </summary>
		/// <param name="id">Id of group</param>
		/// <returns>Group</returns>
		[HttpGet("{id}")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<GroupDto>> TakeGroupByIdAsync(Guid id)
		{
			var result = await _groupProvider.TakeGroupByIdAsync(id);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Create new group
		/// </summary>
		/// <param name="model">Model to create group</param>
		/// <returns>New group</returns>
		[HttpPost]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<GroupDto>> CreateGroupAsync(CreateGroupModel model)
		{
			var result = await _groupProvider.CreateGroupAsync(model);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Update group by id
		/// </summary>
		/// <param name="id">Existed group id</param>
		/// <param name="model">Model to create group</param>
		/// <returns>Updated group</returns>
		[HttpPut("{id}")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<GroupDto>> CreateGroupAsync(Guid id, UpdateGroupModel model)
		{
			var result = await _groupProvider.UpdateGroupAsync(id, model);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Delete group by id
		/// </summary>
		/// <param name="id">Existed group id</param>
		/// <returns>Status message</returns>
		[HttpDelete("{id}")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<GroupDto>> DeleteGroupAsync(Guid id)
		{
			var result = await _groupProvider.DeleteGroupAsync(id);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}
	}
}
