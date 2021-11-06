using Kitias.Persistence.DTOs;
using Kitias.Providers.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
		public ActionResult<IEnumerable<TeacherDto>> TakeStudents()
		{
			var result = _teacherProvider.TakeTeachers();

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
		public async Task<ActionResult<TeacherDto>> TakeStudentByIdAsync(Guid id)
		{
			var result = await _teacherProvider.TakeTeacherByIdAsync(id);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}
	}
}
