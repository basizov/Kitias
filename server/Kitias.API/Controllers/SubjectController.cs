using Kitias.Persistence.DTOs;
using Kitias.Persistence.Enums;
using Kitias.Providers.Interfaces;
using Kitias.Providers.Models.Subject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Kitias.API.Controllers
{
	/// <summary>
	/// Controller to work with subjecct db
	/// </summary>
	public class SubjectController : BaseController
	{
		private readonly ISubjectProvider _subjectProvider;

		/// <summary>
		/// Add services to the app
		/// </summary>
		/// <param name="logger">Logging</param>
		/// <param name="subjectProvider">Provider to work with subjects</param>
		public SubjectController(ILogger<SubjectController> logger, ISubjectProvider subjectProvider) : base(logger) => _subjectProvider = subjectProvider;

		/// <summary>
		/// Take all subject from db
		/// </summary>
		/// <returns>Subjects</returns>
		[HttpGet]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public ActionResult<IEnumerable<SubjectDto>> TakeSubject()
		{
			var result = _subjectProvider.TakeSubjects();

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Take subject from db by id
		/// </summary>
		/// <param name="id">Id of subject</param>
		/// <returns>Subject</returns>
		[HttpGet("{id}")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<SubjectDto>> TakeSubjectByIdAsync(Guid id)
		{
			var result = await _subjectProvider.TakeSubjectByIdAsync(id);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Take sheduler of the subject from db by id
		/// </summary>
		/// <param name="name">Name of subject</param>
		/// <returns>Sheduler</returns>
		[HttpGet("{name}/sheduler")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<AttendanceShedulerDto>> TakeSubjectShedulerAsync(string name)
		{
			var result = await _subjectProvider.TakeSubjectShedulerAsync(name);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Take subject groups from db by id
		/// </summary>
		/// <param name="id">Id of subject</param>
		/// <returns>Groups</returns>
		[HttpGet("{id}/groups")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<IEnumerable<GroupDto>>> TakeGroupSubjectsByIdAsync(Guid id)
		{
			var result = await _subjectProvider.TakeSubjectGroupsAsync(id);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Create new subject
		/// </summary>
		/// <param name="model">Model to create subject</param>
		/// <returns>New subject</returns>
		[HttpPost]
		[Authorize(Roles = RolesNames.TEACHER_ROLE)]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<IEnumerable<SubjectDto>>> CreateSubjectAsync(IEnumerable<CreateSubjectModel> models)
		{
			var result = await _subjectProvider.CreateSubjectAsync(models, User.FindFirst(ClaimTypes.Email)?.Value ?? "");

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Add subject groups to db by id
		/// </summary>
		/// <param name="id">Id of subject</param>
		/// <param name="groups">New groups</param>
		/// <returns>Groups</returns>
		[HttpPost("{id}/groups")]
		[Produces("application/json")]
		[Authorize(Roles = RolesNames.ADMIN_ROLE)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<GroupDto>> AddGroupSubjectsByIdAsync(Guid id, [FromBody] IEnumerable<Guid> groups)
		{
			var result = await _subjectProvider.CreateSubjectGroupsAsync(id, groups);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Update subject by id
		/// </summary>
		/// <param name="id">Existed subject id</param>
		/// <param name="model">Model to create subject</param>
		/// <returns>Updated subject</returns>
		[HttpPut("{id}")]
		[Authorize(Roles = RolesNames.TEACHER_ROLE)]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<GroupDto>> UpdateSubjectAsync(Guid id, UpdateSubjectModel model)
		{
			var result = await _subjectProvider.UpdateSubjectAsync(id, model);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Update subjects by name
		/// </summary>
		/// <param name="model">Model with names for subjects</param>
		/// <returns>Groups</returns>
		[HttpPut]
		[Authorize(Roles = RolesNames.TEACHER_ROLE)]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<IEnumerable<SubjectDto>>> UpdateSubjectsByName(UpdateSubjectByName model)
		{
			var result = await _subjectProvider.UpdateSubjectsByNameAsync(model.Name, model.NewName);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Update subjects by name
		/// </summary>
		/// <param name="name">Name for subjects</param>
		/// <returns>STatus message</returns>
		[HttpDelete("name/{name}")]
		[Authorize(Roles = RolesNames.TEACHER_ROLE)]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<string>> DeleteSubjectsByName(string name)
		{
			var result = await _subjectProvider.DeleteSubjectsByNameAsync(
				name,
				User.FindFirst(ClaimTypes.Email)?.Value ?? ""
			);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Delete subject by id
		/// </summary>
		/// <param name="id">Existed subject id</param>
		/// <returns>Status message</returns>
		[HttpDelete("{id}")]
		[Authorize(Roles = RolesNames.TEACHER_ROLE)]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<GroupDto>> DeleteDeleteAsync(Guid id)
		{
			var result = await _subjectProvider.DeleteSubjectAsync(id);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Delete groups from the subject by id
		/// </summary>
		/// <param name="id">Existed subject id</param>
		/// <param name="groups">Deleted groups</param>
		/// <returns>Status message</returns>
		[HttpDelete("{id}/groups")]
		[Authorize(Roles = RolesNames.ADMIN_ROLE)]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<string>> DeleteGroupSubjectsAsync(Guid id, [FromBody] IEnumerable<Guid> groups)
		{
			var result = await _subjectProvider.DeleteSubjectGroupsAsync(id, groups);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}
	}
}
