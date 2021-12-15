using AutoMapper;
using Kitias.Persistence.DTOs;
using Kitias.Persistence.Entities.Scheduler;
using Kitias.Persistence.Entities.Scheduler.Attendence;
using Kitias.Persistence.Enums;
using Kitias.Providers.Interfaces;
using Kitias.Providers.Models;
using Kitias.Providers.Models.Subject;
using Kitias.Repository.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
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

		public async Task<Result<IEnumerable<SubjectDto>>> CreateSubjectAsync(IEnumerable<CreateSubjectModel> subjects, string email)
		{
			var teacher = await _unitOfWork.Teacher
				.FindByAndInclude(t => t.Person.Email == email, p => p.Person)
				.SingleOrDefaultAsync();

			if (teacher == null)
				return ReturnFailureResult<IEnumerable<SubjectDto>>($"Couldn't find teacher with email {email}", "Couldn't find teacher");
			var sheduler = await _unitOfWork.ShedulerAttendace
				.FindByAndInclude(
					s => s.SubjectName == subjects.First().Name,
					s => s.StudentAttendances
				).SingleOrDefaultAsync();
			List<string> students = null;

			if (sheduler != null)
			{
				students = sheduler.StudentAttendances
					.Select(sa => sa.StudentName)
					.Distinct()
					.ToList();
			}

			//if (await _unitOfWork.Subject
			//		.AnyAsync(s => s.Name == subject.Name && s.Type == Helpers.GetEnumMemberFromString<SubjectType>(subject.Type)))
			//	return ReturnFailureResult<SubjectDto>("This subject have the same subject");
			return await TryCatchExecute(subjects, async (parameter) =>
			{
				var subjectsEntities = new List<Subject>();

				foreach (var subject in parameter)
				{
					var subjectEntity = _mapper.Map<Subject>(subject);

					subjectEntity.TeacherId = teacher.Id;
					var newSubject = _unitOfWork.Subject.Create(subjectEntity);

					subjectsEntities.Add(newSubject);
					if (students != null)
					{
						foreach (var student in students)
						{
							var newAttendance = new Attendance
							{
								ShedulerId = sheduler.Id,
								Score = byte.Parse("0"),
								Attended = Helpers.GetEnumMemberFromString<AttendaceVariants>("Н"),
								SubjectId = subjectEntity.Id,
								StudentName = student
							};

							_unitOfWork.Attendance.Create(newAttendance);
						}
					}
				}
				var isSave = await _unitOfWork.SaveChangesAsync();

				if (isSave <= 0)
					throw new ApplicationException("Couldn't save new subjects");
				var result = _mapper.Map<IEnumerable<SubjectDto>>(subjectsEntities);

				_logger.LogInformation($"Subjects  was successfully created");
				return ResultHandler.OnSuccess(result);
			});
		}


		public async Task<Result<IEnumerable<SubjectDto>>> UpdateSubjectsByNameAsync(string name, string newName)
		{
			var subjects = _unitOfWork.Subject.FindBy(s => s.Name == name);

			if (subjects == null)
				return ReturnFailureResult<IEnumerable<SubjectDto>>($"Couldn't find subjects with name {name}", "Couldn't find subjects");
			return await TryCatchExecute(subjects, async (parameter) =>
			{
				var subjectsEntities = new List<Subject>();

				foreach (var subject in parameter)
				{
					var subjectEntity = _mapper.Map<Subject>(subject);

					subjectEntity.Name = newName;
					var newSubject = _unitOfWork.Subject.Update(subjectEntity);

					subjectsEntities.Add(newSubject);
				}
				var shedulers = await _unitOfWork.ShedulerAttendace
					.FindBy(s => s.SubjectName == name)
					.ToListAsync();

				if (shedulers != null)
				{
					foreach (var sheduler in shedulers)
					{
						sheduler.SubjectName = newName;
						_unitOfWork.ShedulerAttendace.Update(sheduler);
					}
				}
				var isSave = await _unitOfWork.SaveChangesAsync();

				if (isSave <= 0)
					throw new ApplicationException("Couldn't save subjects");
				var result = _mapper.Map<IEnumerable<SubjectDto>>(subjectsEntities);

				_logger.LogInformation($"Subjects  was successfully update");
				return ResultHandler.OnSuccess(result);
			});
		}

		public async Task<Result<string>> DeleteSubjectsByNameAsync(string name, string email)
		{
			var teacher = await _unitOfWork.Teacher
				.FindByAndInclude(
					s => s.Person.Email == email,
					s => s.Person, s => s.Subjects
				)
				.SingleOrDefaultAsync();

			if (teacher == null)
				return ReturnFailureResult<string>($"Couldn't find teacher with email {email}", "Couldn't find teacher");
			return await TryCatchExecute(teacher.Subjects.Where(s => s.Name == name), async (parameter) =>
			{
				foreach (var subject in parameter)
				{
					var subjectEntity = _mapper.Map<Subject>(subject);

					_unitOfWork.Subject.Delete(subjectEntity);
				}
				var isSave = await _unitOfWork.SaveChangesAsync();

				if (isSave <= 0)
					throw new ApplicationException("Couldn't delete subjects");
				_logger.LogInformation($"Subjects  was successfully deleted");
				return ResultHandler.OnSuccess("Subjects were successfully deleted");
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
				.FindByAndInclude(s => s.Id == id, s => s.Attendances)
				.SingleOrDefaultAsync();

			if (findSubject == null)
				return ReturnFailureResult<SubjectDto>($"Subject with id ${id} doesn't existed", "Couldn't find this subject");
			return await TryCatchExecute(findSubject, async (parameter) =>
			{
				if (subject.Name == null)
					parameter.Name = subject.Name;
				if (subject.Time != null)
					parameter.Time = TimeSpan.Parse(subject.Time);
				if (subject.Type != null)
					parameter.Type = Helpers.GetEnumMemberFromString<SubjectType>(subject.Type);
				if (subject.Week != null)
					parameter.Week = Helpers.GetEnumMemberFromString<Week>(subject.Week);
				if (subject.Date != null)
					parameter.Date = DateTime.ParseExact(subject.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
				if (subject.Day != null)
					parameter.Day = Helpers.GetEnumMemberFromString<DayWeek>(subject.Day);
				if (subject.Theme != null)
					parameter.Theme = subject.Theme;
				if (!subject.IsGiveScore)
				{
					parameter.IsGiveScore = subject.IsGiveScore;
					foreach (var attendance in findSubject.Attendances)
					{
						attendance.Score = 100;
						_unitOfWork.Attendance.Update(attendance);
					}
				}
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

		public async Task<Result<AttendanceShedulerDto>> TakeSubjectShedulerAsync(string name)
		{
			var sheduler = await _unitOfWork.ShedulerAttendace
				.FindByAndInclude(
					s => s.SubjectName == name,
					s => s.Group
				)
				.Include(s => s.Teacher)
				.ThenInclude(s => s.Person)
				.SingleOrDefaultAsync();

			if (sheduler == null)
			{
				return ReturnFailureResult<AttendanceShedulerDto>(
					$"Sheduler with subjectName ${name} doesn't existed",
					"Couldn't find sheduler for this subject"
				);
			}
			var result = _mapper.Map<AttendanceShedulerDto>(sheduler);

			return ResultHandler.OnSuccess(result);
		}
	}
}
