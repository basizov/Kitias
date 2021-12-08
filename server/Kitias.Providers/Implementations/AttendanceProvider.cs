using AutoMapper;
using ClosedXML.Excel;
using Kitias.Persistence.DTOs;
using Kitias.Persistence.Entities.Scheduler;
using Kitias.Persistence.Entities.Scheduler.Attendence;
using Kitias.Persistence.Enums;
using Kitias.Providers.Interfaces;
using Kitias.Providers.Models;
using Kitias.Providers.Models.Attendances;
using Kitias.Repository.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
				.FindByAndInclude(
					s => s.TeacherId == teacher.Id,
					s => s.Group
				);
			var result = _mapper.Map<IEnumerable<ShedulersListResult>>(shedulers);

			_logger.LogInformation($"Take all shedulers of the teacher {email}");
			return ResultHandler.OnSuccess(result);
		}

		public async Task<Result<ShedulersListResult>> TakeTeacherShedulerAsync(Guid id)
		{
			var sheduler = await _unitOfWork.ShedulerAttendace
				.FindByAndInclude(
					s => s.Id == id,
					s => s.Group
				)
				.SingleOrDefaultAsync();

			if (sheduler == null)
				return ReturnFailureResult<ShedulersListResult>($"Couldn't find sheduler with id {id}", "Couldn't find sheduler");
			var result = _mapper.Map<ShedulersListResult>(sheduler);

			_logger.LogInformation($"Take sheduler of with id {id}");
			return ResultHandler.OnSuccess(result);
		}

		public async Task<Result<ShedulerStudentsGroup>> TakeShedulerStudentsGroupAsync(Guid id)
		{
			var sheduler = await _unitOfWork.ShedulerAttendace
				.FindByAndInclude(
					s => s.Id == id,
					s => s.Group, s => s.Attendances
				)
				.SingleOrDefaultAsync();

			if (sheduler == null)
				return ReturnFailureResult<ShedulerStudentsGroup>($"Couldn't find sheduler with id {id}", "Couldn't find sheduler");
			var students = sheduler.Attendances.Select(a => a.StudentName).Distinct();

			_logger.LogInformation($"Take sheduler {id} students and gorup id");
			return ResultHandler.OnSuccess(new ShedulerStudentsGroup
			{
				GroupId = sheduler.GroupId,
				Students = students
			});
		}

		public async Task<Result<IEnumerable<SubjectDto>>> TakeTeacherShedulerSubjectsAsync(Guid id, string email)
		{
			var sheduler = await _unitOfWork.ShedulerAttendace
				.FindBy(s => s.Id == id)
				.SingleOrDefaultAsync();

			if (sheduler == null)
				return ReturnFailureResult<IEnumerable<SubjectDto>>($"Couldn't find sheduler with id {id}", "Couldn't find sheduler");
			var subjects = _unitOfWork.Subject
				.FindBy(s => s.Name == sheduler.SubjectName)
				.Include(s => s.Teacher)
				.ThenInclude(t => t.Person)
				.Where(s => s.Teacher.Person.Email == email)
				.OrderBy(s => s.Date);

			if (subjects == null)
				return ReturnFailureResult<IEnumerable<SubjectDto>>($"Couldn't find subject of sheduler {id}", "Couldn't find subjects");
			var result = _mapper.Map<IEnumerable<SubjectDto>>(subjects);

			_logger.LogInformation($"Take sheduler {id} subjects");
			return ResultHandler.OnSuccess(result);
		}

		public async Task<Result<Dictionary<string, IGrouping<string, AttendanceDto>>>> TakeShedulerAttendancesAsync(Guid id)
		{
			var sheduler = await _unitOfWork.ShedulerAttendace
				.FindBy(s => s.Id == id)
				.Include(s => s.Group)
				.Include(s => s.Attendances)
				.ThenInclude(s => s.Student)
				.ThenInclude(s => s.Person)
				.Include(s => s.Attendances)
				.ThenInclude(s => s.Subject)
				.SingleOrDefaultAsync();

			if (sheduler == null)
				return ReturnFailureResult<Dictionary<string, IGrouping<string, AttendanceDto>>>($"Couldn't find sheduler with id {id}", "Couldn't find sheduler");
			var result = _mapper.Map<IEnumerable<AttendanceDto>>(sheduler.Attendances.OrderBy(r => r.Subject.Date));

			_logger.LogInformation($"Take all attendances of the sheduer {id}");
			return ResultHandler.OnSuccess(result
				.GroupBy(r => r.FullName)
				.OrderBy(r => r.Key)
				.ToDictionary(d => d.Key)
			);
		}

		public async Task<Result<IEnumerable<StudentAttendanceResult>>> TakeShedulerStudentAttendancesAsync(Guid id)
		{
			var sheduler = await _unitOfWork.ShedulerAttendace
				.FindBy(s => s.Id == id)
				.Include(s => s.Group)
				.Include(s => s.Attendances)
				.ThenInclude(s => s.Subject)
				.Include(s => s.StudentAttendances)
				.ThenInclude(s => s.Student)
				.ThenInclude(s => s.Person)
				.SingleOrDefaultAsync();

			if (sheduler == null)
				return ReturnFailureResult<IEnumerable<StudentAttendanceResult>>($"Couldn't find sheduler with id {id}", "Couldn't find sheduler");
			var result = new List<StudentAttendanceResult>();

			foreach (var sAttendance in sheduler.StudentAttendances)
			{
				var studentAttendances = sheduler.Attendances
					.Where(a => a.StudentName == sAttendance.StudentName);

				result.Add(new()
				{
					Id = sAttendance.Id,
					Grade = Helpers.GetEnumMemberAttrValue(sAttendance.Grade),
					Raiting = sAttendance.Raiting.ToString(),
					StudentName = sAttendance.StudentName,
					Lectures = _mapper.Map<IEnumerable<SubjectTypeInfo>>(studentAttendances.Where(a => a.Subject.Type == SubjectType.Lecture).OrderBy(s => s.Subject.Date)),
					Laborotories = _mapper.Map<IEnumerable<SubjectTypeInfo>>(studentAttendances.Where(a => a.Subject.Type == SubjectType.Laborotory).OrderBy(s => s.Subject.Date)),
					Practises = _mapper.Map<IEnumerable<SubjectTypeInfo>>(studentAttendances.Where(a => a.Subject.Type == SubjectType.Practise).OrderBy(s => s.Subject.Date))
				});
			}

			_logger.LogInformation($"Take all student attendances of the scheduler {id}");
			return ResultHandler.OnSuccess(result.OrderBy(s => s.StudentName) as IEnumerable<StudentAttendanceResult>);
		}

		public async Task<Result<AttendanceShedulerDto>> CreateShedulerAsync(ShedulerProviderRequestModel model)
		{
			var teacher = await _unitOfWork.Teacher
				.FindByAndInclude(
					t => t.Person.Email == model.TeacherEmail,
					p => p.Person
				).SingleOrDefaultAsync();

			if (teacher == null)
			{
				return ReturnFailureResult<AttendanceShedulerDto>(
					$"Couldn't find teacher with email {model.TeacherEmail}",
					"Couldn't find teacher"
				);
			}
			var subject = await _unitOfWork.Subject
				.FindBy(s => s.Name == model.SubjectName)
				.FirstOrDefaultAsync();

			if (subject == null)
			{
				return ReturnFailureResult<AttendanceShedulerDto>(
					$"Couldn't find subject with name {model.SubjectName}",
					"Couldn't find subject"
				);
			}
			_logger.LogInformation($"Found teacher with id {teacher.Id}");
			var group = await _unitOfWork.Group
				.FindByAndInclude(g => g.Id == model.GroupNumber)
				.SingleOrDefaultAsync();

			if (group != null)
				_logger.LogInformation($"Found group with id {group.Id}");
			var newAttendance = new AttendanceSheduler
			{
				TeacherId = teacher.Id,
				Name = model.Name,
				GroupId = group?.Id ?? null,
				SubjectName = model.SubjectName
			};

			return await TryCatchExecute(newAttendance, async (parameter) =>
			{
				var newSheduler = _unitOfWork.ShedulerAttendace.Create(parameter);
				var isSave = await _unitOfWork.SaveChangesAsync();

				if (isSave <= 0)
					throw new ApplicationException("Couldn't save new sheduler");
				newSheduler.Teacher = teacher;
				newSheduler.Group = group;
				var result = _mapper.Map<AttendanceShedulerDto>(newSheduler);

				_logger.LogInformation($"Sheduler with id {newSheduler.Id} was successfully created");
				return ResultHandler.OnSuccess(result);
			});
		}

		public async Task<Result<AttendanceShedulerDto>> UpdateShedulerAsync(Guid id, ShedulerRequestModel model)
		{
			var sheduler = await _unitOfWork.ShedulerAttendace
				.FindBy(s => s.Id == id)
				.Include(s => s.Teacher)
				.ThenInclude(s => s.Person)
				.Include(s => s.Group)
				.SingleOrDefaultAsync();

			if (sheduler == null)
			{
				return ReturnFailureResult<AttendanceShedulerDto>(
					$"Couldn't find sheduler with id {id}",
					"Couldn't find sheduler"
				);
			}
			Group group = null;

			if (model.GroupNumber != null)
			{
				group = await _unitOfWork.Group
					.FindByAndInclude(g => g.Id == model.GroupNumber)
					.SingleOrDefaultAsync();

				if (group != null)
					_logger.LogInformation($"Found group with id {group.Id}");
				sheduler.GroupId = group.Id;
			}
			if (model.Name != null)
				sheduler.Name = model.Name;
			return await TryCatchExecute(sheduler, async (parameter) =>
			{
				var newSheduler = _unitOfWork.ShedulerAttendace.Update(parameter);
				var isSave = await _unitOfWork.SaveChangesAsync();

				if (isSave <= 0)
					throw new ApplicationException("Couldn't update sheduler");
				if (group != null)
					newSheduler.Group = group;
				var result = _mapper.Map<AttendanceShedulerDto>(newSheduler);

				_logger.LogInformation($"Sheduler with id {newSheduler.Id} was successfully created");
				return ResultHandler.OnSuccess(result);
			});
		}

		public async Task<Result<string>> DeleteShedulerAsync(Guid id)
		{
			var sheduler = await _unitOfWork.ShedulerAttendace
				.FindBy(s => s.Id == id)
				.SingleOrDefaultAsync();

			if (sheduler == null)
			{
				return ReturnFailureResult<string>(
					$"Couldn't find sheduler with id {id}",
					"Couldn't find sheduler"
				);
			}
			return await TryCatchExecute(sheduler, async (parameter) =>
			{
				_unitOfWork.ShedulerAttendace.Delete(parameter);
				var isSave = await _unitOfWork.SaveChangesAsync();

				if (isSave <= 0)
					throw new ApplicationException("Couldn't delete sheduler");
				_logger.LogInformation($"Sheduler with id {id} is successfully deleted");
				return ResultHandler.OnSuccess("Sheduler successfully deleted");
			});
		}

		public async Task<Result<IEnumerable<StudentAttendanceDto>>> CreateStudentAttendanceAsync(Guid id, IEnumerable<StudentAttendanceRequestModel> models)
		{
			if (!await _unitOfWork.ShedulerAttendace.AnyAsync(s => s.Id == id))
			{
				return ReturnFailureResult<IEnumerable<StudentAttendanceDto>>(
					$"Couldn't find sheduler with id {id}",
					"Couldn't find sheduler"
				);
			}
			return await TryCatchExecute(models, async (parameter) =>
			{
				var attendances = new List<StudentAttendance>();

				foreach (var model in parameter)
				{
					var newStudentAttendance = new StudentAttendance
					{
						Grade = Helpers.GetEnumMemberFromString<Grade>(model.Grade ?? "-"),
						Raiting = byte.Parse(model.Raiting ?? "0"),
						ShedulerId = id,
						SubjectName = model.SubjectName
					};

					if (model.StudentId != null)
					{
						var student = await _unitOfWork.Student
							.FindBy(s => s.Id == model.StudentId)
							.Include(s => s.Person)
							.SingleOrDefaultAsync();

						if (student == null)
						{
							return ReturnFailureResult<IEnumerable<StudentAttendanceDto>>(
								$"Couldn't find student with id {model.StudentId}",
								"Couldn't find student"
							);
						}
						newStudentAttendance.StudentId = model.StudentId;
						_unitOfWork.StudentAttendace.Create(newStudentAttendance);
						newStudentAttendance.Student = student;
						attendances.Add(newStudentAttendance);
					}
					else if (model.StudentName != null)
					{
						newStudentAttendance.StudentName = model.StudentName;
						_unitOfWork.StudentAttendace.Create(newStudentAttendance);
						attendances.Add(newStudentAttendance);
					}
					else
						return ReturnFailureResult<IEnumerable<StudentAttendanceDto>>("Enter students");
					_logger.LogInformation($"Created new studenAttendance with id {newStudentAttendance.Id}");
				}
				var isSave = await _unitOfWork.SaveChangesAsync();

				if (isSave <= 0)
					throw new ApplicationException("Couldn't save new student attendances");
				var result = _mapper.Map<IEnumerable<StudentAttendanceDto>>(attendances);

				_logger.LogInformation("Student attendance was successfully created");
				return ResultHandler.OnSuccess(result);
			});
		}

		public async Task<Result<StudentAttendanceDto>> UpdateStudentAttendanceAsync(Guid id, UpdateStudentAttendanceModel model)
		{
			var studentAttendance = await _unitOfWork.StudentAttendace
				.FindBy(s => s.Id == id)
				.Include(s => s.Student)
				.ThenInclude(s => s.Person)
				.SingleOrDefaultAsync();

			if (studentAttendance == null)
			{
				return ReturnFailureResult<StudentAttendanceDto>(
					$"Couldn't find student attendance with id {id}",
					"Couldn't find student attendance"
				);
			}
			if (model.Grade != null)
				studentAttendance.Grade = Helpers.GetEnumMemberFromString<Grade>(model.Grade);
			if (model.Raiting != null)
				studentAttendance.Raiting = byte.Parse(model.Raiting);
			return await TryCatchExecute(studentAttendance, async (parameter) =>
			{
				var updateStudentAttendance = _unitOfWork.StudentAttendace.Update(parameter);
				var isSave = await _unitOfWork.SaveChangesAsync();

				if (isSave <= 0)
					throw new ApplicationException("Couldn't update student attendance");
				var result = _mapper.Map<StudentAttendanceDto>(updateStudentAttendance);

				_logger.LogInformation($"Student attendance {id} was successfully updated");
				return ResultHandler.OnSuccess(result);
			});
		}

		public async Task<Result<string>> DeleteStudentAttendanceAsync(Guid id)
		{
			var studentAttendance = await _unitOfWork.StudentAttendace
				.FindBy(s => s.Id == id)
				.SingleOrDefaultAsync();

			if (studentAttendance == null)
			{
				return ReturnFailureResult<string>(
					$"Couldn't find student attendance with id {id}",
					"Couldn't find student attendance"
				);
			}
			return await TryCatchExecute(studentAttendance, async (parameter) =>
			{
				_unitOfWork.StudentAttendace.Delete(parameter);
				var isSave = await _unitOfWork.SaveChangesAsync();

				if (isSave <= 0)
					throw new ApplicationException("Couldn't delete student attendance");
				_logger.LogInformation($"Student attendance with id {id} is successfully deleted");
				return ResultHandler.OnSuccess("Student attendance successfully deleted");
			});
		}

		public async Task<Result<IEnumerable<AttendanceDto>>> CreateAttendancesAsync(Guid id, IEnumerable<AttendanceRequestModel> models)
		{
			if (!await _unitOfWork.ShedulerAttendace.AnyAsync(s => s.Id == id))
			{
				return ReturnFailureResult<IEnumerable<AttendanceDto>>(
					$"Couldn't find sheduler with id {id}",
					"Couldn't find sheduler"
				);
			}
			return await TryCatchExecute(models, async (parameter) =>
			{
				var attendances = new List<Attendance>();

				foreach (var model in parameter)
				{
					var subject = await _unitOfWork.Subject
						.FindBy(s => s.Id == model.SubjectId)
						.SingleOrDefaultAsync();

					if (subject == null)
					{
						return ReturnFailureResult<IEnumerable<AttendanceDto>>(
							$"Couldn't find subject with id {model.SubjectId}",
							"Couldn't find subject"
						);
					}
					var newAttendance = new Attendance
					{
						ShedulerId = id,
						Score = byte.Parse(model.Score ?? "0"),
						Attended = Helpers.GetEnumMemberFromString<AttendaceVariants>(model.Attended ?? "Н"),
						SubjectId = model.SubjectId
					};

					if (model.StudentId != null)
					{
						var student = await _unitOfWork.Student
							.FindBy(s => s.Id == model.StudentId)
							.Include(s => s.Person)
							.SingleOrDefaultAsync();

						if (student == null)
						{
							return ReturnFailureResult<IEnumerable<AttendanceDto>>(
								$"Couldn't find student with id {model.StudentId}",
								"Couldn't find student"
							);
						}
						newAttendance.StudentId = model.StudentId;
						_unitOfWork.Attendance.Create(newAttendance);
						newAttendance.Student = student;
						newAttendance.Subject = subject;
						attendances.Add(newAttendance);
					}
					else if (model.StudentName != null)
					{
						newAttendance.StudentName = model.StudentName;
						_unitOfWork.Attendance.Create(newAttendance);
						newAttendance.Subject = subject;
						attendances.Add(newAttendance);
					}
					else
						return ReturnFailureResult<IEnumerable<AttendanceDto>>("Enter students");
					_logger.LogInformation($"Created new attendance with id {newAttendance.Id}");
				}
				var isSave = await _unitOfWork.SaveChangesAsync();

				if (isSave <= 0)
					throw new ApplicationException("Couldn't save new attendances");
				var result = _mapper.Map<IEnumerable<AttendanceDto>>(attendances);

				_logger.LogInformation("Attendance was successfully created");
				return ResultHandler.OnSuccess(result);
			});
		}


		public async Task<Result<IEnumerable<AttendanceDto>>> UpdateAttendancesAsync(Guid id, IEnumerable<AttendanceRequestModel> models)
		{
			var sheduler = await _unitOfWork.ShedulerAttendace
				.FindByAndInclude(s => s.Id == id, s => s.Attendances)
				.SingleOrDefaultAsync();

			if (sheduler == null)
			{
				return ReturnFailureResult<IEnumerable<AttendanceDto>>(
					$"Couldn't find sheduler with id {id}",
					"Couldn't find sheduler"
				);
			}
			return await TryCatchExecute(models, async (parameter) =>
			{
				var attendances = new List<Attendance>();

				foreach (var model in parameter)
				{
					if (await _unitOfWork.Attendance
						.AnyAsync(a => a.StudentName == model.StudentName && a.SubjectId == model.SubjectId)
					)
						continue;
					var subject = await _unitOfWork.Subject
						.FindBy(s => s.Id == model.SubjectId)
						.SingleOrDefaultAsync();

					if (subject == null)
					{
						return ReturnFailureResult<IEnumerable<AttendanceDto>>(
							$"Couldn't find subject with id {model.SubjectId}",
							"Couldn't find subject"
						);
					}
					var newAttendance = new Attendance
					{
						ShedulerId = id,
						Score = byte.Parse(model.Score ?? "0"),
						Attended = Helpers.GetEnumMemberFromString<AttendaceVariants>(model.Attended ?? "Н"),
						SubjectId = model.SubjectId
					};

					if (model.StudentId != null)
					{
						var student = await _unitOfWork.Student
							.FindBy(s => s.Id == model.StudentId)
							.Include(s => s.Person)
							.SingleOrDefaultAsync();

						if (student == null)
						{
							return ReturnFailureResult<IEnumerable<AttendanceDto>>(
								$"Couldn't find student with id {model.StudentId}",
								"Couldn't find student"
							);
						}
						newAttendance.StudentId = model.StudentId;
						_unitOfWork.Attendance.Create(newAttendance);
						newAttendance.Student = student;
						newAttendance.Subject = subject;
						attendances.Add(newAttendance);
					}
					else if (model.StudentName != null)
					{
						newAttendance.StudentName = model.StudentName;
						_unitOfWork.Attendance.Create(newAttendance);
						newAttendance.Subject = subject;
						attendances.Add(newAttendance);
					}
					else
						return ReturnFailureResult<IEnumerable<AttendanceDto>>("Enter students");
					_logger.LogInformation($"Created new attendance with id {newAttendance.Id}");
				}
				var oldAttendances = sheduler.Attendances
					.Where(a => !models.Any(m => m.StudentName == a.StudentName && m.SubjectId == a.SubjectId))
					.ToList();

				foreach (var attendance in oldAttendances)
					_unitOfWork.Attendance.Delete(attendance);
				var isSave = await _unitOfWork.SaveChangesAsync();

				if (isSave <= 0)
					throw new ApplicationException("Couldn't save new attendances");
				var result = _mapper.Map<IEnumerable<AttendanceDto>>(attendances);

				_logger.LogInformation("Attendance was successfully created");
				return ResultHandler.OnSuccess(result);
			});
		}

		public async Task<Result<AttendanceDto>> UpdateAttendanceAsync(Guid id, UpdateAttendanceModel model)
		{
			var attendance = await _unitOfWork.Attendance
				.FindBy(a => a.Id == id)
				.Include(a => a.Student)
				.ThenInclude(a => a.Person)
				.Include(a => a.Subject)
				.SingleOrDefaultAsync();

			if (attendance == null)
			{
				return ReturnFailureResult<AttendanceDto>(
					$"Couldn't find attendance with id {id}",
					"Couldn't find attendance"
				);
			}
			if (model.Attended != null)
				attendance.Attended = Helpers.GetEnumMemberFromString<AttendaceVariants>(model.Attended);
			if (model.Score != null)
				attendance.Score = byte.Parse(model.Score);
			return await TryCatchExecute(attendance, async (parameter) =>
			{
				var updateAttendance = _unitOfWork.Attendance.Update(parameter);
				var isSave = await _unitOfWork.SaveChangesAsync();

				if (isSave <= 0)
					throw new ApplicationException("Couldn't update attendance");
				var result = _mapper.Map<AttendanceDto>(updateAttendance);

				_logger.LogInformation($"Attendance {id} was successfully updated");
				return ResultHandler.OnSuccess(result);
			});
		}

		public async Task<Result<string>> DeleteAttendanceAsync(Guid id)
		{
			var attendance = await _unitOfWork.Attendance
				.FindBy(a => a.Id == id)
				.SingleOrDefaultAsync();

			if (attendance == null)
			{
				return ReturnFailureResult<string>(
					$"Couldn't find attendance with id {id}",
					"Couldn't find attendance"
				);
			}
			return await TryCatchExecute(attendance, async (parameter) =>
			{
				_unitOfWork.Attendance.Delete(parameter);
				var isSave = await _unitOfWork.SaveChangesAsync();

				if (isSave <= 0)
					throw new ApplicationException("Couldn't delete attendance");
				_logger.LogInformation($"Attendance with id {id} is successfully deleted");
				return ResultHandler.OnSuccess("Attendance successfully deleted");
			});
		}

		public async Task<Result<byte[]>> ExportShedulerAsync(string name)
		{
			var sheduler = await _unitOfWork.ShedulerAttendace
				.FindBy(s => s.SubjectName == name)
				.Include(s => s.Attendances)
				.ThenInclude(a => a.Subject)
				.Include(s => s.StudentAttendances)
				.SingleOrDefaultAsync();

			if (sheduler == null)
			{
				return ReturnFailureResult<byte[]>(
					$"Couldn't find sheduler with subjectname {name}",
					"Couldn't find sheduler"
				);
			}
			using var workbook = new XLWorkbook();
			var worksheet = workbook.Worksheets.Add(sheduler.Name);
			var (currentRow, currentColumn) = (1, 1);

			worksheet.Cell(currentRow, currentColumn).Value = sheduler.Name;
			worksheet.Range(
				worksheet.Cell(currentRow, currentColumn),
				worksheet.Cell(currentRow + 1, currentColumn)
			).Merge();
			++currentColumn;
			var subjects = await _unitOfWork.Subject
				.FindBy(s => s.Name == name)
				.OrderBy(s => s.Date)
				.ToListAsync();

			if (subjects == null)
			{
				return ReturnFailureResult<byte[]>(
					$"Couldn't find subjects with name {sheduler.SubjectName}",
					"Couldn't find subjects"
				);
			}
			var lectures = subjects.Where(s => s.Type == SubjectType.Lecture).ToList();
			var practises = subjects.Where(s => s.Type == SubjectType.Practise).ToList();
			var laborotories = subjects.Where(s => s.Type == SubjectType.Laborotory).ToList();

			for (var i = 0; i < lectures.Count; ++i)
				worksheet.Cell(currentRow, currentColumn++).Value = $"Лекция {i + 1}";
			for (var i = 0; i < practises.Count; ++i)
				worksheet.Cell(currentRow, currentColumn++).Value = $"Практика {i + 1}";
			for (var i = 0; i < laborotories.Count; ++i)
				worksheet.Cell(currentRow, currentColumn++).Value = $"Лабороторная работа {i + 1}";
			worksheet.Cell(currentRow, currentColumn).Value = "Итого";
			worksheet.Range(
				worksheet.Cell(currentRow, currentColumn),
				worksheet.Cell(currentRow + 1, currentColumn)
			).Merge();
			++currentRow;
			currentColumn = 2;
			foreach (var lecture in lectures)
				worksheet.Cell(currentRow, currentColumn++).Value = lecture.Date.ToString("dd.MM.yyyy");
			foreach (var practise in practises)
				worksheet.Cell(currentRow, currentColumn++).Value = practise.Date.ToString("dd.MM.yyyy");
			foreach (var laborotory in laborotories)
				worksheet.Cell(currentRow, currentColumn++).Value = laborotory.Date.ToString("dd.MM.yyyy");
			currentColumn = 1;
			++currentRow;
			var groupingAttendances = sheduler.Attendances
				.GroupBy(a => a.StudentName)
				.ToList();

			foreach (var attendace in groupingAttendances)
			{
				worksheet.Cell(currentRow, currentColumn++).Value = attendace.Key;
				var value = attendace.ToList();
				var lecturesAtt = value
					.Where(a => a.Subject.Type == SubjectType.Lecture)
					.OrderBy(a => a.Subject.Date)
					.ToList();
				var practisesAtt = value
					.Where(a => a.Subject.Type == SubjectType.Practise)
					.OrderBy(a => a.Subject.Date)
					.ToList();
				var laborotoriesAtt = value
					.Where(a => a.Subject.Type == SubjectType.Laborotory)
					.OrderBy(a => a.Subject.Date)
					.ToList();

				foreach (var item in lecturesAtt)
				{
					worksheet.Cell(currentRow, currentColumn).Value = item.Score;
					worksheet.Cell(currentRow, currentColumn).Style.Fill.BackgroundColor = ColorizedCell(item.Attended);
					++currentColumn;
				}
				foreach (var item in practisesAtt)
				{
					worksheet.Cell(currentRow, currentColumn).Value = item.Score;
					worksheet.Cell(currentRow, currentColumn).Style.Fill.BackgroundColor = ColorizedCell(item.Attended);
					++currentColumn;
				}
				foreach (var item in laborotoriesAtt)
				{
					worksheet.Cell(currentRow, currentColumn).Value = item.Score;
					worksheet.Cell(currentRow, currentColumn).Style.Fill.BackgroundColor = ColorizedCell(item.Attended);
					++currentColumn;
				}
				var sAttendace = await _unitOfWork.StudentAttendace
					.FindBy(s => s.StudentName == attendace.Key && s.SubjectName == sheduler.SubjectName)
					.SingleOrDefaultAsync();

				worksheet.Cell(currentRow, currentColumn).Value = Helpers.GetEnumMemberAttrValue(sAttendace.Grade);
				worksheet.Cell(currentRow, currentColumn).Style.Fill.BackgroundColor = ColorizedCell(sAttendace.Raiting);
				currentColumn = 1;
				++currentRow;
			}
			using var stream = new MemoryStream();

			workbook.SaveAs(stream);
			var result = stream.ToArray();

			return ResultHandler.OnSuccess(result);
		}

		private static XLColor ColorizedCell(AttendaceVariants variant)
		{
			if (variant == AttendaceVariants.Was)
				return XLColor.Green;
			else if (variant == AttendaceVariants.WasIll)
				return XLColor.YellowGreen;
			else if (variant == AttendaceVariants.WasNotByReason)
				return XLColor.Yellow;
			return XLColor.Red;
		}

		private static XLColor ColorizedCell(byte result)
		{
			if (result > 80)
				return XLColor.Green;
			else if (result > 71)
				return XLColor.YellowGreen;
			else if (result > 50)
				return XLColor.Yellow;
			return XLColor.Red;
		}
	}
}
