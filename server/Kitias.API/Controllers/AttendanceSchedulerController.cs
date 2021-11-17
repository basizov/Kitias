using Kitias.Persistence.DTOs;
using Kitias.Providers.Interfaces;
using Kitias.Providers.Models.Attendances;
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
		public async Task<ActionResult<IEnumerable<AttendanceDto>>> TakeShedulerAttendancesByIdAsync(Guid id)
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
	}
}
