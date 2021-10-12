using Kitias.API.Interfaces;
using Kitias.API.Models;
using Kitias.Persistence.Models;
using Kitias.Repository.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Kitias.API.Services
{
	public class UserService : IUserService
	{
		private readonly IUnitOfWork _unitOfWork;

		public UserService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

		public async Task<User> AuthenticateAsync(LoginModel loginModel)
		{
			if (string.IsNullOrEmpty(loginModel.Email) || string.IsNullOrEmpty(loginModel.Password))
				return null;

			var user = await _unitOfWork.User
				.FindBy(u => string.Equals(u.Email, loginModel.Email))
				.SingleOrDefaultAsync();

			if (user == null)
				return null;
			else if (string.Equals(user.PasswordHash, GeneratePasswordHash(loginModel.Password)))
				return null;
			return user;
		}

		public async Task<User> CreateAsync(RegisterModel registerModel)
		{
			if (string.IsNullOrEmpty(registerModel.Email) || string.IsNullOrEmpty(registerModel.Password))
				return null;
			var user = await _unitOfWork.User
				.FindBy(u => string.Equals(u.Email, registerModel.Email))
				.FirstOrDefaultAsync();

			if (user != null)
				return null;
			user = new()
			{
				Email = registerModel.Email,
				UserName = registerModel.Email,
				PasswordHash = GeneratePasswordHash(registerModel.Password)
			};
			_unitOfWork.User.Create(user);
			var	result = await _unitOfWork.SaveChangesAsync();

			return result > 0 ? user : null;
		}

		private string GeneratePasswordHash(string password)
		{
			if (password == null)
				throw new ArgumentNullException("Password mustn't be null");
			else if (string.IsNullOrWhiteSpace(password))
				throw new ArgumentException("Password mustn't be empty or whitespace");
			using var hmac = new HMACSHA512();
			var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

			return Convert.ToBase64String(passwordHash);
		}
	}
}
