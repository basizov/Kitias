using AutoMapper;
using Kitias.Persistence.DTOs;
using Kitias.Persistence.Entities.People;
using Kitias.Providers.Interfaces;
using Kitias.Providers.Models;
using Kitias.Providers.Models.Person;
using Kitias.Repository.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
	}
}
