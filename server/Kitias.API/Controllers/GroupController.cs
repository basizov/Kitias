using Kitias.Persistence.DTOs;
using Kitias.Persistence.Enums;
using Kitias.Providers.Interfaces;
using Kitias.Providers.Models.Group;
using Microsoft.AspNetCore.Authorization;
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
		/// Take all groups names from db
		/// </summary>
		/// <returns>Groups names</returns>
		[HttpGet("names")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public ActionResult<IEnumerable<GroupNames>> TakeGroupsNames()
		{
			var result = _groupProvider.TakeGroupsNames();

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
		/// Take group students from db by id
		/// </summary>
		/// <param name="id">Id of group</param>
		/// <returns>Students</returns>
		[HttpGet("{id}/students")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<IEnumerable<StudentDto>>> TakeGroupStudentsByIdAsync(Guid id)
		{
			var result = await _groupProvider.TakeGroupStudentsAsync(id);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Take group students from db by id
		/// </summary>
		/// <param name="id">Id of group</param>
		/// <returns>Students</returns>
		[HttpGet("{id}/students/names")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<IEnumerable<string>>> TakeGroupStudentsNamesByIdAsync(Guid id)
		{
			var result = await _groupProvider.TakeGroupStudentsNamesAsync(id);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Add group students to db by id
		/// </summary>
		/// <param name="id">Id of group</param>
		/// <param name="students">New students</param>
		/// <returns>Students</returns>
		[HttpPost("{id}/students")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<StudentDto>> AddGroupStudentsByIdAsync(Guid id, [FromBody] IEnumerable<Guid> students)
		{
			var result = await _groupProvider.CreateGroupStudentsAsync(id, students);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Take group subjects from db by id
		/// </summary>
		/// <param name="id">Id of group</param>
		/// <returns>Subjects</returns>
		[HttpGet("{id}/subjects")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<IEnumerable<SubjectDto>>> TakeGroupSubjectsByIdAsync(Guid id)
		{
			var result = await _groupProvider.TakeGroupSubjectsAsync(id);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Add group subjects to db by id
		/// </summary>
		/// <param name="id">Id of group</param>
		/// <param name="subjects">New subjects</param>
		/// <returns>Subjects</returns>
		[HttpPost("{id}/subjects")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<SubjectDto>> AddGroupSubjectsByIdAsync(Guid id, [FromBody] IEnumerable<Guid> subjects)
		{
			var result = await _groupProvider.CreateGroupSubjectsAsync(id, subjects);

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
		[Authorize(Roles = RolesNames.ADMIN_ROLE)]
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
		[Authorize(Roles = RolesNames.ADMIN_ROLE)]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<GroupDto>> UpdateGroupAsync(Guid id, UpdateGroupModel model)
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
		[Authorize(Roles = RolesNames.ADMIN_ROLE)]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<string>> DeleteGroupAsync(Guid id)
		{
			var result = await _groupProvider.DeleteGroupAsync(id);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Delete subjects from the group by id
		/// </summary>
		/// <param name="id">Existed group id</param>
		/// <param name="subjects">Deleted subjects</param>
		/// <returns>Status message</returns>
		[HttpDelete("{id}/subjects")]
		[Authorize(Roles = RolesNames.ADMIN_ROLE)]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<string>> DeleteGroupSubjectsAsync(Guid id, [FromBody] IEnumerable<Guid> subjects)
		{
			var result = await _groupProvider.DeleteGroupSubjectsAsync(id, subjects);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}
	}
}
