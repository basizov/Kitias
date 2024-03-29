﻿using AutoMapper;
using Kitias.Persistence.DTOs;
using Kitias.Persistence.Entities.People;
using Kitias.Persistence.Entities.Scheduler;
using Kitias.Persistence.Enums;
using Kitias.Providers.Interfaces;
using Kitias.Providers.Models;
using Kitias.Providers.Models.Group;
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
	/// Provider to work with group entity
	/// </summary>
	public class GroupProvider : Provider, IGroupProvider
	{
		/// <summary>
		/// Configure all necessary services
		/// </summary>
		/// <param name="mapper">Mapper</param>
		/// <param name="logger">Logging</param>
		/// <param name="unitOfWork">Working with different dbs</param>
		public GroupProvider(IMapper mapper, ILogger<GroupProvider> logger, IUnitOfWork unitOfWork) : base(mapper, logger, unitOfWork) { }

		public Result<IEnumerable<GroupDto>> TakeGroups()
		{
			var groups = _unitOfWork.Group.GetAll();
			var result = _mapper.Map<IEnumerable<GroupDto>>(groups);

			_logger.LogInformation("Take all groups from db");
			return ResultHandler.OnSuccess(result);
		}

		public async Task<Result<IEnumerable<GroupWithStudents>>> TakeGroupsWithStudentsAsync()
		{
			var groups = await _unitOfWork.Group
				.GetAll()
				.Include(g => g.Students)
				.ToListAsync();
			var result = new List<GroupWithStudents>();

			foreach (var group in groups)
			{
				var students = new List<StudentInGroup>();

				foreach (var student in group.Students)
				{
					var person = await _unitOfWork.Person
						.FindBy(p => p.Id == student.PersonId)
						.SingleOrDefaultAsync();

					if (person == null && student.PersonId != null)
					{
						return ReturnFailureResult<IEnumerable<GroupWithStudents>>(
							$"Pereson with id ${student.PersonId} doesn't existed",
							"Couldn't find group student"
						);
					}
					students.Add(new()
					{
						Id = student.Id,
						FullName = student.FullName ?? person.FullName
					});
				}
				result.Add(new()
				{
					Id = group.Id,
					Course = group.Course,
					Number = group.Number,
					Students = students
				});
			}
			_logger.LogInformation("Take all groups with students from db");
			return ResultHandler.OnSuccess(result as IEnumerable<GroupWithStudents>);
		}

		public Result<IEnumerable<GroupNames>> TakeGroupsNames()
		{
			var groups = _unitOfWork.Group.GetAll();
			var result = _mapper.Map<IEnumerable<GroupNames>>(groups);

			_logger.LogInformation("Take all groups from db");
			return ResultHandler.OnSuccess(result);
		}

		public async Task<Result<IEnumerable<StudentDto>>> TakeGroupStudentsAsync(Guid id)
		{
			var group = await _unitOfWork.Group
				.FindBy(g => g.Id == id)
				.Include(g => g.Students)
				.ThenInclude(s => s.Person)
				.SingleOrDefaultAsync();

			if (group == null)
				return ReturnFailureResult<IEnumerable<StudentDto>>($"Group with id ${id} doesn't existed", "Couldn't find this group");
			else if (group.Students == null)
				return ReturnFailureResult<IEnumerable<StudentDto>>($"Couldn;t find students for {id}", "Couldn't find students for this group");
			var result = _mapper.Map<IEnumerable<StudentDto>>(group.Students);

			_logger.LogInformation($"Get students for group {id}");
			return ResultHandler.OnSuccess(result);
		}

		public async Task<Result<IEnumerable<StudentInGroup>>> TakeGroupStudentsNamesAsync(Guid id)
		{
			var group = await _unitOfWork.Group
				.FindBy(g => g.Id == id)
				.Include(g => g.Students)
				.ThenInclude(s => s.Person)
				.SingleOrDefaultAsync();

			if (group == null)
				return ReturnFailureResult<IEnumerable<StudentInGroup>>($"Group with id ${id} doesn't existed", "Couldn't find this group");
			else if (group.Students == null)
				return ReturnFailureResult<IEnumerable<StudentInGroup>>($"Couldn;t find students for {id}", "Couldn't find students for this group");
			_logger.LogInformation($"Get students for group {id}");
			return ResultHandler.OnSuccess(group.Students.Select(s => new StudentInGroup
			{
				Id = s.Id,
				FullName = s.FullName ?? s.Person.FullName
			}));
		}

		public async Task<Result<IEnumerable<SubjectDto>>> TakeGroupSubjectsAsync(Guid id)
		{
			var groupSubjects = await _unitOfWork.SubjectGroup
				.FindBy(g => g.GroupId == id)
				.ToListAsync();

			if (groupSubjects == null)
				return ReturnFailureResult<IEnumerable<SubjectDto>>($"GroupSubject with id ${id} doesn't existed", "Couldn't find subjects for this group");
			var result = new List<SubjectDto>();

			foreach (var groupSubject in groupSubjects)
			{
				var subject = await _unitOfWork.Subject
					.FindBy(s => s.Id == groupSubject.SubjectId)
					.SingleOrDefaultAsync();

				if (subject == null)
					return ReturnFailureResult<IEnumerable<SubjectDto>>($"Couldn't find subject with id ${groupSubject.SubjectId} doesn't existed", "Couldn't find subject");
				result.Add(_mapper.Map<SubjectDto>(subject));
				_logger.LogInformation($"Add subject {groupSubject.SubjectId} to group {id}");
			}
			_logger.LogInformation($"Get subjects for group {id}");
			return ResultHandler.OnSuccess(result as IEnumerable<SubjectDto>);
		}

		public async Task<Result<GroupDto>> TakeGroupByIdAsync(Guid id)
		{
			var group = await _unitOfWork.Group
				.FindBy(g => g.Id == id)
				.SingleOrDefaultAsync();

			if (group == null)
				return ReturnFailureResult<GroupDto>($"Group with id ${id} doesn't existed", "Couldn't find this group");
			var result = _mapper.Map<GroupDto>(group);

			_logger.LogInformation($"Take group {id} from db");
			return ResultHandler.OnSuccess(result);
		}

		public async Task<Result<GroupDto>> CreateGroupAsync(CreateGroupModel group)
		{
			if (await _unitOfWork.Group.AnyAsync(g => g.Number == group.Number))
				return ReturnFailureResult<GroupDto>("Group with same number is existed");
			return await TryCatchExecute(group, async (parameter) =>
			{
				var groupEntity = _mapper.Map<Group>(parameter);
				var newGroup = _unitOfWork.Group.Create(groupEntity);
				var isSave = await _unitOfWork.SaveChangesAsync();

				if (isSave <= 0)
					throw new ApplicationException("Couldn't save new group");
				var result = _mapper.Map<GroupDto>(newGroup);

				_logger.LogInformation($"Group with id {newGroup.Id} was successfully created");
				return ResultHandler.OnSuccess(result);
			});
		}

		public async Task<Result<IEnumerable<string>>> CreateGroupStudentsAsync(Guid id, IEnumerable<string> students)
		{
			return await TryCatchExecute(students, async (parameter) =>
			{
				var group = await _unitOfWork.Group
					.FindBy(g => g.Id == id)
					.Include(g => g.Students)
					.SingleOrDefaultAsync();

				if (group == null)
				{
					return ReturnFailureResult<IEnumerable<string>>(
						$"Group with id ${id} doesn't existed",
						"Couldn't find this group"
					);
				}
				if (group.Students == null)
					group.Students = new List<Student>();
				foreach (var student in group.Students)
				{
					var person = await _unitOfWork.Person
						.FindBy(p => p.Id == student.PersonId)
						.SingleOrDefaultAsync();

					if (person == null && student.PersonId != null)
					{
						return ReturnFailureResult<IEnumerable<string>>(
							$"Person with id ${student.PersonId} doesn't existed",
							"Couldn't find student"
						);
					}
					student.Person = person;
				}
				foreach (var studentId in parameter)
				{
					Student findStudent = null;

					if (Guid.TryParse(studentId, out var sId))
					{
						if (group.Students.Any(s => s.Id == sId))
							continue;
						findStudent = await _unitOfWork.Student
							.FindBy(s => s.Id == sId)
							.Include(s => s.Person)
							.SingleOrDefaultAsync();
					}
					else
					{
						if (group.Students.Any(s => s.FullName == studentId))
							continue;
						findStudent = _unitOfWork.Student.Create(new()
						{
							FullName = studentId,
							GroupId = group.Id
						});
						group.Students.Add(findStudent);
					}
					if (findStudent == null)
					{
						return ReturnFailureResult<IEnumerable<string>>(
							$"Student with id ${studentId} doesn't existed",
							"Couldn't find student"
						);
					}
					if (findStudent.GroupId != group.Id)
					{
						group.Students.Add(findStudent);
						findStudent.GroupId = group.Id;
						_unitOfWork.Student.Update(findStudent);
						_logger.LogInformation($"Add student {studentId} to the group {id}");
					}
				}
				var oldStudents = group.Students
					.Where(s => !parameter.Contains(s.FullName) && !parameter.Contains(s.Id.ToString()))
					.ToList();

				foreach (var oldStudent in oldStudents)
				{
					group.Students.Remove(oldStudent);
					_unitOfWork.Student.Delete(oldStudent);
				}
				await EditAttendances(group.Id, parameter);
				var isSave = await _unitOfWork.SaveChangesAsync();

				if (isSave <= 0)
					throw new ApplicationException("Couldn't save new students");
				var result = group.Students.Select(s => s.FullName ?? s.Person.FullName);

				_logger.LogInformation($"Get all students of the group {id}");
				return ResultHandler.OnSuccess(result);
			});
		}

		private async Task EditAttendances(Guid groupId, IEnumerable<string> students)
		{
			var shedulers = await _unitOfWork.ShedulerAttendace
				.FindByAndInclude(
					s => s.GroupId == groupId,
					s => s.Attendances, s => s.StudentAttendances
				).ToListAsync();

			if (shedulers == null)
				return;
			foreach (var sheduler in shedulers)
			{
				var subjects = await _unitOfWork.Subject
					.FindBy(s => s.TeacherId == sheduler.TeacherId && s.Name == sheduler.SubjectName)
					.ToListAsync();

				if (subjects == null || subjects.Count == 0)
					continue;
				foreach (var student in students)
				{
					if (Guid.TryParse(student, out var sId))
					{
						if (await _unitOfWork.StudentAttendace
							.AnyAsync(sa => sa.StudentId == sId))
							continue;
						_unitOfWork.StudentAttendace.Create(new()
						{
							Grade = Helpers.GetEnumMemberFromString<Grade>("-"),
							Raiting = byte.Parse("0"),
							ShedulerId = sheduler.Id,
							SubjectName = sheduler.SubjectName,
							StudentId = sId
						});

						foreach (var subject in subjects)
						{
							_unitOfWork.Attendance.Create(new()
							{
								ShedulerId = sheduler.Id,
								Score = byte.Parse("0"),
								Attended = Helpers.GetEnumMemberFromString<AttendaceVariants>("Н"),
								SubjectId = subject.Id,
								StudentId = sId
							});
						}
					}
					else
					{
						if (await _unitOfWork.StudentAttendace
							.AnyAsync(sa => sa.StudentName == student))
							continue;
						_unitOfWork.StudentAttendace.Create(new()
						{
							Grade = Helpers.GetEnumMemberFromString<Grade>("-"),
							Raiting = byte.Parse("0"),
							ShedulerId = sheduler.Id,
							SubjectName = sheduler.SubjectName,
							StudentName = student
						});

						foreach (var subject in subjects)
						{
							_unitOfWork.Attendance.Create(new()
							{
								ShedulerId = sheduler.Id,
								Score = byte.Parse("0"),
								Attended = Helpers.GetEnumMemberFromString<AttendaceVariants>("Н"),
								SubjectId = subject.Id,
								StudentName = student
							});
						}
					}
				}

				var oldStudentAttendances = sheduler.StudentAttendances
					.Where(a => !students.Any(s => s == a.StudentName || s == a.StudentId.ToString()))
					.ToList();

				foreach (var sAttendance in oldStudentAttendances)
					_unitOfWork.StudentAttendace.Delete(sAttendance);
				var oldAttendances = sheduler.Attendances
					.Where(a => !students.Any(s => s== a.StudentName || s == a.StudentId.ToString()))
					.ToList();

				foreach (var attendance in oldAttendances)
					_unitOfWork.Attendance.Delete(attendance);
			}
		}

		public async Task<Result<IEnumerable<SubjectDto>>> CreateGroupSubjectsAsync(Guid id, IEnumerable<Guid> subjects)
		{
			var subjectGroups = await _unitOfWork.SubjectGroup
				.FindBy(g => g.GroupId == id)
				.Include(g => g.Subject)
				.ToListAsync();

			if (subjectGroups == null)
			{
				return ReturnFailureResult<IEnumerable<SubjectDto>>(
					$"Couldn;t find subject for group {id}",
					"There isn't this subjects for this group"
				);
			}
			return await TryCatchExecute(subjectGroups, async (parameter) =>
			{
				foreach (var subject in subjects)
				{
					if (!subjectGroups.Any(sg => sg.SubjectId == subject))
					{
						var subjectGroup = new SubjectGroup
						{
							GroupId = id,
							SubjectId = subject
						};
						var subjectEntity = await _unitOfWork.Subject
							.FindBy(s => s.Id == subject)
							.SingleOrDefaultAsync();

						if (subjectEntity == null)
							return ReturnFailureResult<IEnumerable<SubjectDto>>($"Couldn't find subject with id ${subject} doesn't existed", "Couldn't find subject");
						_unitOfWork.SubjectGroup.Create(subjectGroup);
						subjectGroup.Subject = subjectEntity;
						subjectGroups.Add(subjectGroup);
						_logger.LogInformation($"Add subject {subject} ot the group {id}");
					}
				}
				var isSave = await _unitOfWork.SaveChangesAsync();

				if (isSave <= 0)
					throw new ApplicationException("Couldn't save new subjects");
				var result = _mapper.Map<IEnumerable<SubjectDto>>(subjectGroups.Select(sg => sg.Subject));

				_logger.LogInformation($"Get all subjects of the group {id}");
				return ResultHandler.OnSuccess(result);
			});
		}

		public async Task<Result<GroupDto>> UpdateGroupAsync(Guid id, UpdateGroupModel group)
		{
			var findGroup = await _unitOfWork.Group
				.FindBy(g => g.Id == id)
				.Include(g => g.Students)
				.ThenInclude(s => s.Person)
				.SingleOrDefaultAsync();

			if (findGroup == null)
			{
				return ReturnFailureResult<GroupDto>(
					$"Group with id ${id} doesn't existed",
					"Couldn't find this group"
				);
			}
			findGroup.Course = group.Course ?? findGroup.Course;
			findGroup.Number = group.Number ?? findGroup.Number;
			return await TryCatchExecute(findGroup, async (parameter) =>
			{
				var updateGroup = _unitOfWork.Group.Update(parameter);
				var isSave = await _unitOfWork.SaveChangesAsync();

				if (isSave <= 0)
					throw new ApplicationException($"Couldn't update group with id ${id}");
				var result = _mapper.Map<GroupDto>(updateGroup);

				_logger.LogInformation($"Group with id {id} was successfully updated");
				return ResultHandler.OnSuccess(result);
			});
		}

		public async Task<Result<string>> DeleteGroupAsync(Guid id)
		{
			var findGroup = await _unitOfWork.Group
				.FindBy(g => g.Id == id)
				.SingleOrDefaultAsync();

			if (findGroup == null)
				return ReturnFailureResult<string>($"Group with id ${id} doesn't existed", "Couldn't find this group");
			return await TryCatchExecute(findGroup, async (parameter) =>
			{
				_unitOfWork.Group.Delete(parameter);
				var isSave = await _unitOfWork.SaveChangesAsync();

				if (isSave <= 0)
					throw new ApplicationException($"Couldn't delete group with id ${id}");
				_logger.LogInformation($"Group with id {id} was successfully deleted");
				return ResultHandler.OnSuccess("Group was successfully deleted");
			});
		}

		public async Task<Result<string>> DeleteGroupSubjectsAsync(Guid id, IEnumerable<Guid> subjects)
		{
			var findGroupSubjects = await _unitOfWork.SubjectGroup
				.FindBy(g => g.GroupId == id)
				.ToListAsync();

			if (findGroupSubjects == null)
				return ReturnFailureResult<string>($"GroupSubject with groupId ${id} doesn't existed", "Couldn't find this subjects of the group");
			return await TryCatchExecute(findGroupSubjects, async (parameter) =>
			{
				foreach (var groupSubject in parameter)
				{
					if (subjects.Contains(groupSubject.SubjectId))
					{
						_unitOfWork.SubjectGroup.Delete(groupSubject);
						_logger.LogInformation($"Subject {groupSubject.SubjectId} was successfully deleted from the group {id}");
					}
					else
						return ReturnFailureResult<string>($"Subject {groupSubject.SubjectId} doesn't exist in group {id}", "Couldn't find subject");
				}
				var isSave = await _unitOfWork.SaveChangesAsync();

				if (isSave <= 0)
					throw new ApplicationException($"Couldn't delete subjects from the group ${id}");
				_logger.LogInformation($"Subjects was successfully deleted from the group {id}");
				return ResultHandler.OnSuccess("Subjects was successfully deleted from the group");
			});
		}
	}
}
