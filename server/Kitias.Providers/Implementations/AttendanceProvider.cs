using AutoMapper;
using Kitias.Persistence.DTOs;
using Kitias.Providers.Interfaces;
using Kitias.Providers.Models;
using Kitias.Repository.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitias.Providers.Implementations
{
	/// <summary>
	/// Provider to work with attendances
	/// </summary>
	public class AttendanceProvider : Provider, IAttendanceProvider
	{
		/// <summary>
		/// Constructor to get services
		/// </summary>
		/// <param name="mapper">Mapping</param>
		/// <param name="logger">Logging</param>
		/// <param name="unitOfWork">Class to work with different dbs</param>
		public AttendanceProvider(IMapper mapper, ILogger<Provider> logger, IUnitOfWork unitOfWork) : base(mapper, logger, unitOfWork)
		{ }

		public async Task<Result<IEnumerable<Guid>>> TakeTeacherShedulersAsync(string email)
		{
			var teacher = await _unitOfWork.Teacher
				.FindByAndInclude(t => t.Person.Email == email, p => p.Person)
				.SingleOrDefaultAsync();

			if (teacher == null)
				return ReturnFailureResult<IEnumerable<Guid>>($"Couldn't find teacher with email {email}", "Couldn't find teacher");
			var shedulers = _unitOfWork.ShedulerAttendace
				.FindBy(s => s.TeacherId == teacher.Id)
				.Select(s => s.Id)
				.AsEnumerable();

			return ResultHandler.OnSuccess(shedulers);
		}

		public async Task<Result<IEnumerable<AttendanceDto>>> TakeShedulerAttendancesAsync(Guid id)
		{
			var sheduler = await _unitOfWork.ShedulerAttendace
				.FindByAndInclude(
					s => s.Id == id,
					s => s.StudentAttendances, s => s.Students
				).SingleOrDefaultAsync();

			if (sheduler == null)
				return ReturnFailureResult<IEnumerable<AttendanceDto>>($"Couldn't find sheduler with id {id}", "Couldn't find sheduler");
		}

		private Result<T> ReturnFailureResult<T>(string loggerMessage, string errorMessage = null)
			where T : class
		{
			_logger.LogError(loggerMessage);
			return ResultHandler.OnFailure<T>(errorMessage ?? loggerMessage);
		}
	}
}
