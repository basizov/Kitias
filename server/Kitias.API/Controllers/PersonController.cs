using AutoMapper;
using Kitias.Persistence.DTOs;
using Kitias.Repository.Interfaces.Base;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Kitias.API.Controllers
{
	public class PersonController : BaseController
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public PersonController(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		/// <summary>
		/// Get all persong of this application
		/// </summary>
		/// <returns>Persons</returns>
		[HttpGet]
		public ActionResult<IEnumerable<PersonDto>> GetPersons()
		{
			var users = _unitOfWork.Person.GetAll();

			return Ok(_mapper.Map<IEnumerable<PersonDto>>(users));
		}

		/// <summary>
		/// Get only students of this application
		/// </summary>
		/// <returns>Students</returns>
		[HttpGet("students")]
		public ActionResult<IEnumerable<StudentDto>> GetStudents()
		{
			var users = _unitOfWork.Student.GetAll();

			return Ok(_mapper.Map<IEnumerable<StudentDto>>(users));
		}

		/// <summary>
		/// Get only teachers of this application
		/// </summary>
		/// <returns>Teachers</returns>
		[HttpGet("teachers")]
		public ActionResult<IEnumerable<TeacherDto>> GetTeachers()
		{
			var users = _unitOfWork.Teacher.GetAll();

			return Ok(_mapper.Map<IEnumerable<TeacherDto>>(users));
		}
	}
}
