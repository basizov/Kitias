using Kitias.Persistence.DTOs;
using Kitias.Providers.Interfaces;
using Kitias.Providers.Models.Attendances;
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
	/// Controller to work with attendances
	/// </summary>
	public class AttendanceSchedulerController : BaseController
	{
		private readonly IAttendanceProvider _attendanceProvider;

		/// <summary>
		/// Constructor to get servies
		/// </summary>
		/// <param name="logger">Logging</param>
		/// <param name="attendanceProvider">Provider to work with data contexta nd attendances</param>
		public AttendanceSchedulerController(ILogger<AttendanceSchedulerController> logger, IAttendanceProvider attendanceProvider) : base(logger) => _attendanceProvider = attendanceProvider;

		/// <summary>
		/// Take all shedulers of the teacher
		/// </summary>
		/// <returns>Shedulers</returns>
		[HttpGet]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<IEnumerable<ShedulersListResult>>> TakeShedulersAsync()
		{
			var result = await _attendanceProvider.TakeTeacherShedulersAsync(User.FindFirst(ClaimTypes.Email)?.Value ?? "");

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Take sheduler by id
		/// </summary>
		/// <param name="id">Sheduler identifier</param>
		/// <returns>Sheduler</returns>
		[HttpGet("{id}")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<SubjectDto>> TakeShedulerAsync(Guid id)
		{
			var result = await _attendanceProvider.TakeTeacherShedulerAsync(id);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Take shedulre subjects by id
		/// </summary>
		/// <param name="id">Sheduler identifier</param>
		/// <returns>Sheduler subjects</returns>
		[HttpGet("{id}/subjects")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<IEnumerable<SubjectDto>>> TakeShedulerSubjectsAsync(Guid id)
		{
			var result = await _attendanceProvider.TakeTeacherShedulerSubjectsAsync(id);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Create sheduler for the teacher
		/// </summary>
		/// <param name="model">Model to create new sheduler</param>
		/// <returns>New sheduler</returns>
		[HttpPost]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<AttendanceShedulerDto>> CreateShedulerAsync(ShedulerRequestModel model)
		{
			var result = await _attendanceProvider.CreateShedulerAsync(new()
			{
				GroupNumber = model.GroupNumber,
				Name = model.Name,
				SubjectName = model.SubjectName,
				TeacherEmail = User.FindFirst(ClaimTypes.Email)?.Value ?? ""
			});

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Update sheduler by id
		/// </summary>
		/// <param name="id">Id of sheduler</param>
		/// <param name="model">Model to update sheduler</param>
		/// <returns>Updated sheduler</returns>
		[HttpPut("{id}")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<IEnumerable<AttendanceDto>>> UpdateShedulerAsync(Guid id, ShedulerRequestModel model)
		{
			var result = await _attendanceProvider.UpdateShedulerAsync(id, model);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Delete sheduler by id
		/// </summary>
		/// <param name="id">Id of sheduler</param>
		/// <returns>Status message</returns>
		[HttpDelete("{id}")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<string>> DeleteShedulerAsync(Guid id)
		{
			var result = await _attendanceProvider.DeleteShedulerAsync(id);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Take all attendances of the sheduler
		/// </summary>
		/// <param name="id">Id of sheduler</param>
		/// <returns>Attendances</returns>
		[HttpGet("{id}/attendances")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<Dictionary<string, IGrouping<string, AttendanceDto>>>> TakeShedulerAttendancesByIdAsync(Guid id)
		{
			var result = await _attendanceProvider.TakeShedulerAttendancesAsync(id);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Take all student attendances of the sheduler
		/// </summary>
		/// <param name="id">Id of sheduler</param>
		/// <returns>Student attendances</returns>
		[HttpGet("{id}/studentAttendances")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<IEnumerable<StudentAttendanceDto>>> TakeShedulerByIdAsync(Guid id)
		{
			var result = await _attendanceProvider.TakeShedulerStudentAttendancesAsync(id);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Create student attendances of the sheduler
		/// </summary>
		/// <param name="id">Id of sheduler</param>
		/// <param name="models">Model to craete student attendace</param>
		/// <returns>Student attendances</returns>
		[HttpPost("{id}/studentAttendances")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<IEnumerable<StudentAttendanceDto>>> CreateStudentAttendancesAsync(Guid id, IEnumerable<StudentAttendanceRequestModel> models)
		{
			var result = await _attendanceProvider.CreateStudentAttendanceAsync(id, models);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Update student attendance
		/// </summary>
		/// <param name="id">Id of student attendace</param>
		/// <param name="model">Model to updart student attendace</param>
		/// <returns>Update student attendance</returns>
		[HttpPut("{id}/studentAttendances")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<IEnumerable<StudentAttendanceDto>>> UpdateStudentAttendanceAsync(Guid id, UpdateStudentAttendanceModel model)
		{
			var result = await _attendanceProvider.UpdateStudentAttendanceAsync(id, model);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Delete student attendance
		/// </summary>
		/// <param name="id">Id of student attendace</param>
		/// <returns>Status message</returns>
		[HttpDelete("{id}/studentAttendances")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<string>> DeleteStudentAttendanceAsync(Guid id)
		{
			var result = await _attendanceProvider.DeleteStudentAttendanceAsync(id);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Create attendances of the sheduler
		/// </summary>
		/// <param name="id">Id of sheduler</param>
		/// <param name="models">Model to craete attendace</param>
		/// <returns>Attendances</returns>
		[HttpPost("{id}/attendances")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<IEnumerable<AttendanceDto>>> CreateAttendancesAsync(Guid id, IEnumerable<AttendanceRequestModel> models)
		{
			var result = await _attendanceProvider.CreateAttendancesAsync(id, models);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Update attendance
		/// </summary>
		/// <param name="id">Id of attendace</param>
		/// <param name="model">Model to update attendace</param>
		/// <returns>Update attendance</returns>
		[HttpPut("{id}/attendances")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<IEnumerable<AttendanceDto>>> UpdateAttendanceAsync(Guid id, UpdateAttendanceModel model)
		{
			var result = await _attendanceProvider.UpdateAttendanceAsync(id, model);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		/// <summary>
		/// Delete attendance
		/// </summary>
		/// <param name="id">Id of attendace</param>
		/// <returns>Status message</returns>
		[HttpDelete("{id}/attendances")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<string>> DeleteAttendanceAsync(Guid id)
		{
			var result = await _attendanceProvider.DeleteAttendanceAsync(id);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}
	}
}
