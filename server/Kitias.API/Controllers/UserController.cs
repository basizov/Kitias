using AutoMapper;
using Kitias.Persistence.DTOs;
using Kitias.Repository.Interfaces.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Kitias.API.Controllers
{
	[Authorize]
	public class UserController : BaseController
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public UserController(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult GetAccounts()
		{
			var users = _unitOfWork.User.GetAll();

			return Ok(_mapper.Map<IList<UserDto>>(users));
		}
	}
}
