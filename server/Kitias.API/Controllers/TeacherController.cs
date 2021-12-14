using Kitias.Persistence.DTOs;
using Kitias.Providers.Interfaces;
using Kitias.Providers.Models.Person;
using Kitias.Providers.Models.Subject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Kitias.API.Controllers
{
	/// <summary>
	/// Controller to work with persons
	/// </summary>
	public class TeacherController : BaseController
	{
		private readonly ITeacherProvider _teacherProvider;

		/// <summary>
		/// Add neccessary services
		/// </summary>
		/// <param name="logger">Logging</param>
		/// <param name="teacherProvider">Teacher provider to work teacher db</param>
		public TeacherController(ILogger<TeacherController> logger, ITeacherProvider teacherProvider) : base(logger) => _teacherProvider = teacherProvider;

		/// <summary>
		/// Take all teachers from db
		/// </summary>
		/// <returns>Teachers</returns>
		[HttpGet]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public ActionResult<IEnumerable<TeacherDto>> TakeTeachers()
		{
			var result = _teacherProvider.TakeTeachers();

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Take all teachers from db
		/// </summary>
		/// <returns>Teachers</returns>
		[HttpGet("shedulers")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<IEnumerable<TeacherShedulerModel>>> TakeTeachersShedulersAsync()
		{
			var result = await _teacherProvider.TakeTeachersShedulersAsync();

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Take teacher from db by id
		/// </summary>
		/// <param name="id">Id of teacher</param>
		/// <returns>Teacher</returns>
		[HttpGet("{id}")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<TeacherDto>> TakeTeacherByIdAsync(Guid id)
		{
			var result = await _teacherProvider.TakeTeacherByIdAsync(id);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Take subjects of the teacher
		/// </summary>
		/// <returns>Subjects</returns>
		[HttpGet("subjects")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<IEnumerable<SubjectDto>>> TakeTeacherSubjectsAsync()
		{
			var result = await _teacherProvider.TakeTeacherSubjectsAsync(User.FindFirst(ClaimTypes.Email)?.Value ?? "");

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Take subjects names of the teacher
		/// </summary>
		/// <returns>Subjects names</returns>
		[HttpGet("subjects/names")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<IEnumerable<string>>> TakeTeacherSubjectsNamesAsync()
		{
			var result = await _teacherProvider.TakeTeacherSubjectsNamesAsync(User.FindFirst(ClaimTypes.Email)?.Value ?? "");

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Take subject of the teacher
		/// </summary>
		/// <param name="name">subject name</param>
		/// <returns>Subjects</returns>
		[HttpGet("subjects/{name}")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<IEnumerable<SubjectDto>>> TakeTeacherSubjectAsync(string name)
		{
			var result = await _teacherProvider.TakeTeacherSubjectAsync(User.FindFirst(ClaimTypes.Email)?.Value ?? "", name);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Take subjects infos of the teacher
		/// </summary>
		/// <returns>Subjects infos</returns>
		[HttpGet("subjectsInfos")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<Dictionary<string, Dictionary<string, IGrouping<string, string>>>>> TakeTeacherSubjectsInfosAsync()
		{
			var result = await _teacherProvider.TakeTeacherSubjectsInfosAsync(User.FindFirst(ClaimTypes.Email)?.Value ?? "");

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}
	}
}
