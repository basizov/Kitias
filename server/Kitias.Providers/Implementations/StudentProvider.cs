using AutoMapper;
using Kitias.Persistence.DTOs;
using Kitias.Persistence.Entities;
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
	/// Provider to work woth Student
	/// </summary>
	public class StudentProvider : Provider, IStudentProvider
	{
		/// <summary>
		/// Constructor to get services
		/// </summary>
		/// <param name="mapper">Mapping</param>
		/// <param name="logger">Logging</param>
		/// <param name="unitOfWork">Class to work with different dbs</param>
		public StudentProvider(IMapper mapper, ILogger<Provider> logger, IUnitOfWork unitOfWork) : base(mapper, logger, unitOfWork) { }

		public Result<IEnumerable<StudentDto>> TakeStudents()
		{
			var student = _unitOfWork.Student
				.GetAllWithInclude(s => s.Group, s => s.Person);
			var result = _mapper.Map<IEnumerable<StudentDto>>(student);

			_logger.LogInformation("Take all students from db");
			return ResultHandler.OnSuccess(result);
		}

		public async Task<Result<StudentDto>> TakeStudentByIdAsync(Guid id)
		{
			var student = await _unitOfWork.Student
				.FindBy(s => s.Id == id)
				.Include(s => s.Group)
				.Include(s => s.Person)
				.SingleOrDefaultAsync();

			if (student == null)
				return ReturnFailureResult<StudentDto>($"Student with id ${id} doesn't existed", "Couldn't find this student");
			var result = _mapper.Map<StudentDto>(student);

			_logger.LogInformation($"Take student {id} from db");
			return ResultHandler.OnSuccess(result);
		}

		public async Task<Result<StudentDto>> CreateStudentAsync(CreateStudentModel student)
		{
			if (await _unitOfWork.Person.AnyAsync(s => s.Email == student.Email))
				return ReturnFailureResult<StudentDto>("Student with same email is existed");
			try
			{
				var group = await _unitOfWork.Group
					.FindBy(g => g.Number == student.GroupNumber)
					.SingleOrDefaultAsync();

				if (group == null && student.GroupNumber != null)
					return ReturnFailureResult<StudentDto>($"Couldn't find group {student.GroupNumber}");
				_logger.LogInformation($"Find group {student.GroupNumber}");
				var person = _mapper.Map<Person>(student);
				var newPerson = _unitOfWork.Person.Create(person);

				_logger.LogInformation($"Create new person with email {student.Email}");
				var studentEntity = _mapper.Map<Student>(student);

				studentEntity.GroupId = group.Id;
				studentEntity.PersonId = newPerson.Id;
				var newStudent = _unitOfWork.Student.Create(studentEntity);
				var isSave = await _unitOfWork.SaveChangesAsync();

				if (isSave <= 0)
					throw new ApplicationException("Couldn't save new student");
				newStudent.Group = group;
				newStudent.Person = person;
				var result = _mapper.Map<StudentDto>(newStudent);

				_logger.LogInformation($"Student with id {newStudent.Id} was successfully created");
				return ResultHandler.OnSuccess(result);
			}
			catch (ApplicationException)
			{
				throw;
			}
			catch (Exception ex)
			{
				return ReturnFailureResult<StudentDto>(ex.Message, "Error student data");
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
