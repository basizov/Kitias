using Kitias.Persistence.DTOs;
using Kitias.Providers.Interfaces;
using Kitias.Providers.Models.Student;
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
	public class StudentController : BaseController
	{
		private readonly IStudentProvider _studentProvider;

		/// <summary>
		/// Add neccessary services
		/// </summary>
		/// <param name="logger">Logging</param>
		/// <param name="studentProvider">Student provider to work student db</param>
		public StudentController(ILogger<StudentController> logger, IStudentProvider studentProvider) : base(logger) => _studentProvider = studentProvider;

		/// <summary>
		/// Take all students from db
		/// </summary>
		/// <returns>Students</returns>
		[HttpGet]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public ActionResult<IEnumerable<StudentDto>> TakeStudents()
		{
			var result = _studentProvider.TakeStudents();

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Take student from db by id
		/// </summary>
		/// <param name="id">Id of student</param>
		/// <returns>Student</returns>
		[HttpGet("{id}")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<StudentDto>> TakeStudentByIdAsync(Guid id)
		{
			var result = await _studentProvider.TakeStudentByIdAsync(id);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}
	}
}
