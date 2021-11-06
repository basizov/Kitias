using AutoMapper;
using Kitias.Persistence.DTOs;
using Kitias.Persistence.Entities;
using Kitias.Persistence.Enums;
using Kitias.Providers.Interfaces;
using Kitias.Providers.Models;
using Kitias.Providers.Models.Subject;
using Kitias.Repository.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kitias.Providers.Implementations
{
	/// <summary>
	/// Provider to work with subject db
	/// </summary>
	public class SubjectProvider : Provider, ISubjectProvider
	{
		/// <summary>
		/// Constructor to get services
		/// </summary>
		/// <param name="mapper">Mapping</param>
		/// <param name="logger">Logging</param>
		/// <param name="unitOfWork">Pattern to get required db</param>
		public SubjectProvider(IMapper mapper, ILogger<Provider> logger, IUnitOfWork unitOfWork) : base(mapper, logger, unitOfWork) { }

		public Result<IEnumerable<SubjectDto>> TakeSubjects()
		{
			var subjects = _unitOfWork.Subject.GetAll();
			var result = _mapper.Map<IEnumerable<SubjectDto>>(subjects);

			_logger.LogInformation("Take all subject from db");
			return ResultHandler.OnSuccess(result);
		}

		public async Task<Result<SubjectDto>> TakeSubjectByIdAsync(Guid id)
		{
			var subject = await _unitOfWork.Subject
				.FindBy(g => g.Id == id)
				.SingleOrDefaultAsync();

			if (subject == null)
				return ReturnFailureResult<SubjectDto>($"Subject with id ${id} doesn't existed", "Couldn't find this subject");
			var result = _mapper.Map<SubjectDto>(subject);

			_logger.LogInformation($"Take subject {id} from db");
			return ResultHandler.OnSuccess(result);
		}

		public async Task<Result<SubjectDto>> CreateSubjectAsync(CreateSubjectModel subject)
		{
			if (await _unitOfWork.Subject
					.AnyAsync(s => s.Name == subject.Name && s.Type == Helpers.GetEnumMemberFromString<SubjectType>(subject.Type)))
				return ReturnFailureResult<SubjectDto>("This subject have the same subject");
			try
			{
				var subjectEntity = _mapper.Map<Subject>(subject);
				var newSubject = _unitOfWork.Subject.Create(subjectEntity);

				if (subject.GroupIds != null)
				{
					foreach (var groupId in subject.GroupIds)
					{
						if (!await _unitOfWork.Group.AnyAsync(g => g.Id == groupId))
							return ReturnFailureResult<SubjectDto>($"Couldn't find group {groupId}", "Couldn't find group");
						else if (await _unitOfWork.SubjectGroup.AnyAsync(gs => gs.SubjectId == newSubject.Id && gs.GroupId == groupId))
							return ReturnFailureResult<SubjectDto>($"Group has the same subject");
						_unitOfWork.SubjectGroup.Create(new()
						{
							GroupId = groupId,
							SubjectId = newSubject.Id
						});
						_logger.LogInformation($"Created group {groupId} subject {newSubject.Id}");
					}
				}
				var isSave = await _unitOfWork.SaveChangesAsync();

				if (isSave <= 0)
					throw new ApplicationException("Couldn't save new subject");
				var result = _mapper.Map<SubjectDto>(newSubject);

				_logger.LogInformation($"Subject with id {newSubject.Id} was successfully created");
				return ResultHandler.OnSuccess(result);
			}
			catch (ApplicationException)
			{
				throw;
			}
			catch (Exception ex)
			{
				return ReturnFailureResult<SubjectDto>(ex.Message, "Error subject data");
			}
		}

		public async Task<Result<SubjectDto>> UpdateSubjectAsync(Guid id, UpdateSubjectModel subject)
		{
			var findSubject = await _unitOfWork.Subject
				.FindBy(s => s.Id == id)
				.SingleOrDefaultAsync();

			if (findSubject == null)
				return ReturnFailureResult<SubjectDto>($"Subject with id ${id} doesn't existed", "Couldn't find this subject");
			try
			{
				var updatedEntity = _mapper.Map<Subject>(subject);

				if (subject.Name != null)
					findSubject.Name = updatedEntity.Name;
				if (subject.Time != null)
					findSubject.Time = updatedEntity.Time;
				if (subject.Type != null)
					findSubject.Type = updatedEntity.Type;
				if (subject.Week != null)
					findSubject.Week = updatedEntity.Week;
				if (subject.Date != null)
					findSubject.Date = updatedEntity.Date;
				if (subject.Day != null)
					findSubject.Day = updatedEntity.Day;
				var updateSubject = _unitOfWork.Subject.Update(findSubject);
				var isSave = await _unitOfWork.SaveChangesAsync();

				if (isSave <= 0)
					throw new ApplicationException($"Couldn't update subject with id ${id}");
				var result = _mapper.Map<SubjectDto>(updateSubject);

				_logger.LogInformation($"Subject with id {id} was successfully updated");
				return ResultHandler.OnSuccess(result);
			}
			catch (ApplicationException)
			{
				throw;
			}
			catch (Exception ex)
			{
				return ReturnFailureResult<SubjectDto>(ex.Message, "Error subject data");
			}
		}

		public async Task<Result<string>> DeleteSubjectAsync(Guid id)
		{
			var findSubject = await _unitOfWork.Subject
				.FindBy(s => s.Id == id)
				.SingleOrDefaultAsync();

			if (findSubject == null)
				return ReturnFailureResult<string>($"Subject with id ${id} doesn't existed", "Couldn't find this subject");
			try
			{
				_unitOfWork.Subject.Delete(findSubject);
				var isSave = await _unitOfWork.SaveChangesAsync();

				if (isSave <= 0)
					throw new ApplicationException($"Couldn't delete subject with id ${id}");
				_logger.LogInformation($"Subject with id {id} was successfully deleted");
				return ResultHandler.OnSuccess("Subject was successfully deleted");
			}
			catch (ApplicationException)
			{
				throw;
			}
			catch (Exception ex)
			{
				return ReturnFailureResult<string>(ex.Message, "Error subject data");
			}
		}

		private Result<T> ReturnFailureResult<T>(string loggerMessage, string errorMessage = null)
			where T : class
		{
			_logger.LogError(loggerMessage);
			return ResultHandler.OnFailure<T>(errorMessage ?? loggerMessage);
		}
	}
}
