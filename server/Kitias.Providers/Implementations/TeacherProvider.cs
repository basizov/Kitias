using AutoMapper;
using Kitias.Persistence.DTOs;
using Kitias.Persistence.Entities.People;
using Kitias.Providers.Interfaces;
using Kitias.Providers.Models;
using Kitias.Providers.Models.Person;
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
	/// Provider to work with teacher db
	/// </summary>
	public class TeacherProvider : Provider, ITeacherProvider
	{
		/// <summary>
		/// Constructor to get services
		/// </summary>
		/// <param name="mapper">Mapping</param>
		/// <param name="logger">Logging</param>
		/// <param name="unitOfWork">Pattern to get work with different dbs</param>
		public TeacherProvider(IMapper mapper, ILogger<Provider> logger, IUnitOfWork unitOfWork) : base(mapper, logger, unitOfWork) { }

		public Result<IEnumerable<TeacherDto>> TakeTeachers()
		{
			var teacher = _unitOfWork.Teacher
				.GetAllWithInclude(s => s.Person);
			var result = _mapper.Map<IEnumerable<TeacherDto>>(teacher);

			_logger.LogInformation("Take all teachers from db");
			return ResultHandler.OnSuccess(result);
		}

		public async Task<Result<IEnumerable<TeacherShedulerModel>>> TakeTeachersShedulersAsync()
		{
			var shedulers = await _unitOfWork.ShedulerAttendace
				.GetAll()
				.Include(s => s.Teacher)
				.ThenInclude(s => s.Person)
				.ToListAsync();
			var result = _mapper.Map<IEnumerable<TeacherShedulerModel>>(shedulers);

			_logger.LogInformation("Take all teachers shedulers from db");
			return ResultHandler.OnSuccess(result);
		}

		public async Task<Result<TeacherDto>> TakeTeacherByIdAsync(Guid id)
		{
			var teacher = await _unitOfWork.Teacher
				.FindBy(s => s.Id == id)
				.Include(s => s.Person)
				.SingleOrDefaultAsync();

			if (teacher == null)
				return ReturnFailureResult<TeacherDto>($"Teacher with id ${id} doesn't existed", "Couldn't find this teacher");
			var result = _mapper.Map<TeacherDto>(teacher);

			_logger.LogInformation($"Take all teacher {id} from db");
			return ResultHandler.OnSuccess(result);
		}

		public async Task<Result<TeacherDto>> CreateTeacherAsync(CreateTeacherModel teacher)
		{
			if (await _unitOfWork.Person.AnyAsync(s => s.Email == teacher.Email))
				return ReturnFailureResult<TeacherDto>("Teacher with same email is existed");
			return await TryCatchExecute(teacher, async (parameter) =>
			{
				var person = _mapper.Map<Person>(parameter);
				var newPerson = _unitOfWork.Person.Create(person);

				_logger.LogInformation($"Create new person with email {teacher.Email}");
				var teacherEntity = _mapper.Map<Teacher>(parameter);

				teacherEntity.PersonId = newPerson.Id;
				var newStudent = _unitOfWork.Teacher.Create(teacherEntity);
				var isSave = await _unitOfWork.SaveChangesAsync();

				if (isSave <= 0)
					throw new ApplicationException("Couldn't save new teacher");
				newStudent.Person = person;
				var result = _mapper.Map<TeacherDto>(newStudent);

				_logger.LogInformation($"Teacher with id {newStudent.Id} was successfully created");
				return ResultHandler.OnSuccess(result);
			});
		}

		public async Task<Result<IEnumerable<SubjectDto>>> TakeTeacherSubjectsAsync(string email)
		{
			var teacher = await _unitOfWork.Teacher
				.FindByAndInclude(
					s => s.Person.Email == email,
					s => s.Person, s => s.Subjects
				)
				.SingleOrDefaultAsync();

			if (teacher == null)
				return ReturnFailureResult<IEnumerable<SubjectDto>>($"Teacher with email ${email} doesn't existed", "Couldn't find this teacher");
			var result = _mapper.Map<IEnumerable<SubjectDto>>(teacher.Subjects);

			_logger.LogInformation($"Take all subjects of teacher {email}");
			return ResultHandler.OnSuccess(result);
		}

		public async Task<Result<IEnumerable<string>>> TakeTeacherSubjectsNamesAsync(string email)
		{
			var teacher = await _unitOfWork.Teacher
				.FindByAndInclude(
					s => s.Person.Email == email,
					s => s.Person, s => s.Subjects
				)
				.SingleOrDefaultAsync();

			if (teacher == null)
				return ReturnFailureResult<IEnumerable<string>>($"Teacher with email ${email} doesn't existed", "Couldn't find this teacher");

			_logger.LogInformation($"Take all subjects names of teacher {email}");
			return ResultHandler.OnSuccess(teacher.Subjects
				.OrderBy(s => s.Date)
				.Select(s => s.Name)
				.Distinct()
			);
		}

		public async Task<Result<IEnumerable<SubjectDto>>> TakeTeacherSubjectAsync(string email, string name)
		{
			var teacher = await _unitOfWork.Teacher
				.FindByAndInclude(
					s => s.Person.Email == email,
					s => s.Person, s => s.Subjects
				)
				.SingleOrDefaultAsync();

			if (teacher == null)
				return ReturnFailureResult<IEnumerable<SubjectDto>>($"Teacher with email ${email} doesn't existed", "Couldn't find this teacher");
			var result = _mapper.Map<IEnumerable<SubjectDto>>(teacher.Subjects
				.OrderBy(s => s.Date)
				.Where(s => s.Name.Equals(name))
			);

			_logger.LogInformation($"Take all subjects of teacher {email}");
			return ResultHandler.OnSuccess(result);
		}

		public async Task<Result<Dictionary<string, Dictionary<string, IGrouping<string, string>>>>> TakeTeacherSubjectsInfosAsync(string email)
		{
			var teacher = await _unitOfWork.Teacher
				.FindByAndInclude(
					s => s.Person.Email == email,
					s => s.Person, s => s.Subjects
				)
				.SingleOrDefaultAsync();

			if (teacher == null)
				return ReturnFailureResult<Dictionary<string, Dictionary<string, IGrouping<string, string>>>>($"Teacher with email ${email} doesn't existed", "Couldn't find this teacher");
			var result = teacher.Subjects
				.Select(s => new SubjectInfoModel
				{
					Type = Helpers.GetEnumMemberAttrValue(s.Type),
					Name = s.Name,
					Date = s.Date.ToString("dd.MM.yyyy"),
					Time = s.Time.ToString().Remove(s.Time.ToString().Length - 3)
				})
				.OrderBy(s => s.Name)
				.GroupBy(s => s.Name)
				.ToDictionary(s => s.Key);
			var r = new Dictionary<string, Dictionary<string, IGrouping<string, string>>>();

			foreach (var key in result.Keys)
			{
				var item = result.GetValueOrDefault(key);
				var newValue = item
					.OrderBy(s => DateTime.ParseExact(
						$"{s.Date} {s.Time}", 
						"dd.MM.yyyy H:mm",
						CultureInfo.InvariantCulture
					))
					.GroupBy(s => s.Type, s => $"{s.Date} {s.Time}")
					.ToDictionary(s => s.Key);

				r.Add(key, newValue);
			}
			_logger.LogInformation($"Take all subjects of teacher {email}");
			return ResultHandler.OnSuccess(r);
		}
	}
}
