using AutoMapper;
using Kitias.Auth.API.Interfaces;
using Kitias.Auth.API.Models;
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

namespace Kitias.Auth.API.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly ILogger<AuthController> _logger;
		private readonly IMapper _mapper;
		private readonly IUserService _userService;
		private readonly ITokensService _tokenService;
		private readonly IUnitOfWork _unitOfWork;

		public AuthController(ILogger<AuthController> logger, IUserService userService, ITokensService tokenService, IMapper mapper, IUnitOfWork unitOfWork)
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
			var accessToken = GenerateAndSetTokensToCookies(user);

			if (accessToken == null)
				return BadRequest("Couldn't create tokens");
			return Ok(new AuthorizationResultModel
			{
				Id = user.Id,
				Email = user.Email,
				FullName = user.FullName,
				AccessToken = accessToken
			});
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
			var accessToken = GenerateAndSetTokensToCookies(userEntity);

			if (accessToken == null)
				return BadRequest("Couldn't create tokens");
			return Ok(accessToken);
		}

		[HttpPost("logout")]
		public IActionResult Logout()
		{
			HttpContext.Response.Cookies.Delete(".AspNetCore.Application.Guid");
			HttpContext.Response.Cookies.Delete(".AspNetCore.Application.Guidance");
			return Ok("Success logout");
		}

		private string GenerateAndSetTokensToCookies(Persistence.Models.User user)
		{
			var tokens = _tokenService.GenerateTokens(user);

			if (tokens == null)
				return null;
			HttpContext.Response.Cookies.Append(
				".AspNetCore.Application.Guid",
				tokens.AccessToken,				new CookieOptions {
					MaxAge = TimeSpan.FromHours(1),
					Domain = ".localhost",
					Path = "/api/account"
				}
			);
			HttpContext.Response.Cookies.Append(
				".AspNetCore.Application.Guidance",
				tokens.RefreshToken,
				new CookieOptions
				{
					MaxAge = TimeSpan.FromDays(7),
					Domain = ".localhost",
					Path = "/api/account"
				}
			);
			return tokens.AccessToken;
		}
	}
}
