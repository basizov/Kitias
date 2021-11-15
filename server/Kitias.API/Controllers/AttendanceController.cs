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
	public class AttendanceController : BaseController
	{
		private readonly IAttendanceProvider _attendanceProvider;

		/// <summary>
		/// Constructor to get servies
		/// </summary>
		/// <param name="logger">Logging</param>
		/// <param name="attendanceProvider">Provider to work with data contexta nd attendances</param>
		public AttendanceController(ILogger<AttendanceController> logger, IAttendanceProvider attendanceProvider) : base(logger) => _attendanceProvider = attendanceProvider;

		/// <summary>
		/// Take all shedulers of the teacher
		/// </summary>
		/// <returns>Shedulers</returns>
		[HttpGet("shedulers")]
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
		/// Take all attendances of the sheduler
		/// </summary>
		/// <param name="id">Id of sheduler</param>
		/// <returns>Attendances</returns>
		[HttpGet("shedulers/{id}")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<IEnumerable<AttendanceDto>>> TakeShedulerByIdAsync(Guid id)
		{
			var result = await _attendanceProvider.TakeShedulerAttendancesAsync(id);

			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}
	}
}
