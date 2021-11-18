using AutoMapper;
using Kitias.Persistence.DTOs;
using Kitias.Persistence.Entities.Scheduler;
using Kitias.Persistence.Enums;
using Kitias.Providers.Interfaces;
using Kitias.Providers.Models;
using Kitias.Providers.Models.Subject;
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

		public async Task<Result<IEnumerable<GroupDto>>> TakeSubjectGroupsAsync(Guid id)
		{
			var groupSubjects = await _unitOfWork.SubjectGroup
				.FindBy(g => g.SubjectId == id)
				.ToListAsync();

			if (groupSubjects == null)
				return ReturnFailureResult<IEnumerable<GroupDto>>($"GroupSubject with id ${id} doesn't existed", "Couldn't find groups for this subject");
			var result = new List<GroupDto>();

			foreach (var groupSubject in groupSubjects)
			{
				var group = await _unitOfWork.Group
					.FindBy(s => s.Id == groupSubject.GroupId)
					.SingleOrDefaultAsync();

				if (group == null)
					return ReturnFailureResult<IEnumerable<GroupDto>>($"Couldn't find group with id ${groupSubject.GroupId} doesn't existed", "Couldn't find group");
				result.Add(_mapper.Map<GroupDto>(group));
				_logger.LogInformation($"Add group {groupSubject.GroupId} to subject {id}");
			}
			_logger.LogInformation($"Get groups for subject {id}");
			return ResultHandler.OnSuccess(result as IEnumerable<GroupDto>);
		}

		public async Task<Result<SubjectDto>> CreateSubjectAsync(CreateSubjectModel subject)
		{
			if (await _unitOfWork.Subject
					.AnyAsync(s => s.Name == subject.Name && s.Type == Helpers.GetEnumMemberFromString<SubjectType>(subject.Type)))
				return ReturnFailureResult<SubjectDto>("This subject have the same subject");
			return await TryCatchExecute(subject, async (parameter) =>
			{
				var subjectEntity = _mapper.Map<Subject>(parameter);
				var newSubject = _unitOfWork.Subject.Create(subjectEntity);
				var isSave = await _unitOfWork.SaveChangesAsync();

				if (isSave <= 0)
					throw new ApplicationException("Couldn't save new subject");
				var result = _mapper.Map<SubjectDto>(newSubject);

				_logger.LogInformation($"Subject with id {newSubject.Id} was successfully created");
				return ResultHandler.OnSuccess(result);
			});
		}

		public async Task<Result<IEnumerable<GroupDto>>> CreateSubjectGroupsAsync(Guid id, IEnumerable<Guid> groups)
		{
			var subjectGroups = await _unitOfWork.SubjectGroup
				.FindBy(g => g.SubjectId == id)
				.Include(g => g.Group)
				.ToListAsync();

			if (subjectGroups == null)
			{
				return ReturnFailureResult<IEnumerable<GroupDto>>(
					$"Couldn't find groups for subject {id}",
					"There isn't this groups for this subject"
				);
			}
			return await TryCatchExecute(subjectGroups, async (parameter) =>
			{
				foreach (var group in groups)
				{
					if (!parameter.Any(sg => sg.GroupId == group))
					{
						var subjectGroup = new SubjectGroup
						{
							SubjectId = id,
							GroupId = group
						};
						var groupEntity = await _unitOfWork.Group
							.FindBy(g => g.Id == group)
							.SingleOrDefaultAsync();

						if (groupEntity == null)
						{
							return ReturnFailureResult<IEnumerable<GroupDto>>(
								$"Couldn't find group with id ${group} doesn't existed",
								"Couldn't find group"
							);
						}
						_unitOfWork.SubjectGroup.Create(subjectGroup);
						subjectGroup.Group = groupEntity;
						parameter.Add(subjectGroup);
						_logger.LogInformation($"Add group {group} ot the subject {id}");
					}
				}
				var isSave = await _unitOfWork.SaveChangesAsync();

				if (isSave <= 0)
					throw new ApplicationException("Couldn't save new groups");
				var result = _mapper.Map<IEnumerable<GroupDto>>(parameter.Select(sg => sg.Group));

				_logger.LogInformation($"Get all groups of the subject {id}");
				return ResultHandler.OnSuccess(result);
			});
		}

		public async Task<Result<SubjectDto>> UpdateSubjectAsync(Guid id, UpdateSubjectModel subject)
		{
			var findSubject = await _unitOfWork.Subject
				.FindBy(s => s.Id == id)
				.SingleOrDefaultAsync();

			if (findSubject == null)
				return ReturnFailureResult<SubjectDto>($"Subject with id ${id} doesn't existed", "Couldn't find this subject");
			return await TryCatchExecute(findSubject, async (parameter) =>
			{
				var updatedEntity = _mapper.Map<Subject>(subject);

				if (subject.Name != null)
					parameter.Name = updatedEntity.Name;
				if (subject.Time != null)
					parameter.Time = updatedEntity.Time;
				if (subject.Type != null)
					parameter.Type = updatedEntity.Type;
				if (subject.Week != null)
					parameter.Week = updatedEntity.Week;
				if (subject.Date != null)
					parameter.Date = updatedEntity.Date;
				if (subject.Day != null)
					parameter.Day = updatedEntity.Day;
				if (subject.Theme != null)
					parameter.Theme = updatedEntity.Theme;
				var updateSubject = _unitOfWork.Subject.Update(parameter);
				var isSave = await _unitOfWork.SaveChangesAsync();

				if (isSave <= 0)
					throw new ApplicationException($"Couldn't update subject with id ${id}");
				var result = _mapper.Map<SubjectDto>(updateSubject);

				_logger.LogInformation($"Subject with id {id} was successfully updated");
				return ResultHandler.OnSuccess(result);
			});
		}

		public async Task<Result<string>> DeleteSubjectAsync(Guid id)
		{
			var findSubject = await _unitOfWork.Subject
				.FindBy(s => s.Id == id)
				.SingleOrDefaultAsync();

			if (findSubject == null)
			{
				return ReturnFailureResult<string>(
					$"Subject with id ${id} doesn't existed",
					"Couldn't find this subject"
				);
			}
			return await TryCatchExecute(findSubject, async (parameter) =>
			{
				_unitOfWork.Subject.Delete(parameter);
				var isSave = await _unitOfWork.SaveChangesAsync();

				if (isSave <= 0)
					throw new ApplicationException($"Couldn't delete subject with id ${id}");
				_logger.LogInformation($"Subject with id {id} was successfully deleted");
				return ResultHandler.OnSuccess("Subject was successfully deleted");
			});
		}
		public async Task<Result<string>> DeleteSubjectGroupsAsync(Guid id, IEnumerable<Guid> groups)
		{
			var findSubjectGroups = await _unitOfWork.SubjectGroup
				.FindBy(g => g.SubjectId == id)
				.ToListAsync();

			if (findSubjectGroups == null)
				return ReturnFailureResult<string>($"GroupSubject with subjectId ${id} doesn't existed", "Couldn't find this groups of the subject");
			return await TryCatchExecute(findSubjectGroups, async (parameter) =>
			{
				foreach (var subjectGroup in parameter)
				{
					if (groups.Contains(subjectGroup.GroupId))
					{
						_unitOfWork.SubjectGroup.Delete(subjectGroup);
						_logger.LogInformation($"Group {subjectGroup.GroupId} was successfully deleted from the subject {id}");
					}
					else
						return ReturnFailureResult<string>($"Group {subjectGroup.GroupId} doesn't exist in subject {id}", "Couldn't find group");
				}
				var isSave = await _unitOfWork.SaveChangesAsync();

				if (isSave <= 0)
					throw new ApplicationException($"Couldn't delete groups from the subject${id}");
				_logger.LogInformation($"Groups was successfully deleted from the subject {id}");
				return ResultHandler.OnSuccess("Groups was successfully deleted from the subject");
			});
		}
	}
}
