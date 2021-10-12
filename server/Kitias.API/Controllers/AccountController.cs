using AutoMapper;
using Kitias.API.Interfaces;
using Kitias.API.Models;
using Kitias.Persistence.DTOs;
using Kitias.Repository.Interfaces.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kitias.API.Controllers
{
	[Authorize]
	public class AccountController : BaseController
	{
		private readonly ILogger<AccountController> _logger;
		private readonly IMapper _mapper;
		private readonly IUserService _userService;
		private readonly ITokensService _tokenService;
		private readonly IUnitOfWork _unitOfWork;

		public AccountController(ILogger<AccountController> logger, IUserService userService, ITokensService tokenService, IMapper mapper, IUnitOfWork unitOfWork)
		{
			_logger = logger;
			_userService = userService;
			_tokenService = tokenService;
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		[AllowAnonymous]
		[HttpPost("login")]
		public async Task<IActionResult> LoginAsync(LoginModel loginModel)
		{
			var user = await _userService.AuthenticateAsync(loginModel);

			if (user == null)
				return BadRequest($"Email is already used");
			else if (!GenerateAndSetTokensToCookies(user))
				return BadRequest("Couldn't create tokens");
			return Ok(_mapper.Map<UserDto>(user));
		}

		[AllowAnonymous]
		[HttpPost("register")]
		public async Task<IActionResult> RegisterAsync(RegisterModel registerModel)
		{
			var user = await _userService.CreateAsync(registerModel);

			if (user == null)
				return BadRequest($"Email is already used");
			return Ok(_mapper.Map<UserDto>(user));
		}

		[AllowAnonymous]
		[HttpPost("refresh")]
		public async Task<IActionResult> RefreshAsync(Guid userId)
		{
			var userEntity = await _unitOfWork.User
				.FindBy(u => Equals(u.Id, userId))
				.SingleOrDefaultAsync();

			if (userEntity == null)
				return BadRequest($"Couldn't find user with same id");
			else if (!GenerateAndSetTokensToCookies(userEntity))
				return BadRequest("Couldn't create tokens");
			return Ok("Refresh tokens");
		}

		[HttpPost("logout")]
		public IActionResult Logout()
		{
			HttpContext.Response.Cookies.Delete(".AspNetCore.Application.Guid");
			HttpContext.Response.Cookies.Delete(".AspNetCore.Application.Guidance");
			return Ok("Success logout");
		}

		[HttpGet]
		public IActionResult GetAccounts()
		{
			var users = _unitOfWork.User.GetAll();

			return Ok(_mapper.Map<IList<UserDto>>(users));
		}

		private bool GenerateAndSetTokensToCookies(Persistence.Models.User user)
		{
			var tokens = _tokenService.GenerateTokens(user);

			if (tokens == null)
				return false;
			HttpContext.Response.Cookies.Append(
				".AspNetCore.Application.Guid",
				tokens.AccessToken,
				new CookieOptions { MaxAge = TimeSpan.FromHours(1) }
			);
			HttpContext.Response.Cookies.Append(
				".AspNetCore.Application.Guidance",
				tokens.RefreshToken,
				new CookieOptions { MaxAge = TimeSpan.FromDays(7) }
			);
			return true;
		}
	}
}
