using AutoMapper;
using Kitias.Persistence.DTOs;
using Kitias.Persistence.Entities.Scheduler.Attendence;
using Kitias.Providers.Interfaces;
using Kitias.Providers.Models;
using Kitias.Providers.Models.Attendances;
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

		public async Task<Result<IEnumerable<ShedulersListResult>>> TakeTeacherShedulersAsync(string email)
		{
			var teacher = await _unitOfWork.Teacher
				.FindByAndInclude(t => t.Person.Email == email, p => p.Person)
				.SingleOrDefaultAsync();

			if (teacher == null)
				return ReturnFailureResult<IEnumerable<ShedulersListResult>>($"Couldn't find teacher with email {email}", "Couldn't find teacher");
			var shedulers = _unitOfWork.ShedulerAttendace
				.FindByAndInclude(s => s.TeacherId == teacher.Id, s => s.Group)
				.ToList();
			var result = _mapper.Map<IEnumerable<ShedulersListResult>>(shedulers);

			_logger.LogInformation($"Take all shedulers of the teacher {email}");
			return ResultHandler.OnSuccess(result);
		}

		public async Task<Result<IEnumerable<AttendanceDto>>> TakeShedulerAttendancesAsync(Guid id)
		{
			var sheduler = await _unitOfWork.ShedulerAttendace
				.FindBy(s => s.Id == id)
				.Include(s => s.StudentAttendances)
				.ThenInclude(s => s.Attendances)
				.ThenInclude(s => s.Subject)
				.Include(s => s.StudentAttendances)
				.ThenInclude(s => s.Attendances)
				.ThenInclude(s => s.Student)
				.ThenInclude(s => s.Person)
				.SingleOrDefaultAsync();

			if (sheduler == null)
				return ReturnFailureResult<IEnumerable<AttendanceDto>>($"Couldn't find sheduler with id {id}", "Couldn't find sheduler");
			var attendancesEntities = new List<Attendance>();
			var result = new List<AttendanceDto>();

			foreach (var studentAttendance in sheduler.StudentAttendances)
			{
				var attendances = studentAttendance.Attendances;

				attendancesEntities.AddRange(attendances);
			}
			result.AddRange(_mapper.Map<IEnumerable<AttendanceDto>>(attendancesEntities.OrderBy(r => r.Date)));
			_logger.LogInformation($"Take all attendances of the sheduer {id}");
			return ResultHandler.OnSuccess(result as IEnumerable<AttendanceDto>);
		}

		private Result<T> ReturnFailureResult<T>(string loggerMessage, string errorMessage = null)
			where T : class
		{
			_logger.LogError(loggerMessage);
			return ResultHandler.OnFailure<T>(errorMessage ?? loggerMessage);
		}
	}
}
