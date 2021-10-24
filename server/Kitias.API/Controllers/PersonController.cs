using AutoMapper;
using Kitias.Persistence.DTOs;
using Kitias.Repository.Interfaces.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Kitias.API.Controllers
{
	[Authorize]
	public class PersonController : BaseController
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public PersonController(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult GetPersons()
		{
			var users = _unitOfWork.Person.GetAll();

			return Ok(_mapper.Map<IList<PersonDto>>(users));
		}

		[HttpGet("students")]
		public IActionResult GetStudents()
		{
			var users = _unitOfWork.Student.GetAll();

			return Ok(_mapper.Map<IList<StudentDto>>(users));
		}

		[HttpGet("teachers")]
		public IActionResult GetTeachers()
		{
			var users = _unitOfWork.Teacher.GetAll();

			return Ok(_mapper.Map<IList<TeacherDto>>(users));
		}
	}
}
