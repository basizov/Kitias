using Kitias.Auth.API.Interfaces;
using Kitias.Auth.API.Models;
using Kitias.Persistence.Models;
using Kitias.Repository.Interfaces.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Kitias.Auth.API.Services
{
	public class TokensService : ITokensService
	{
		private readonly IConfiguration _config;
		private readonly IUnitOfWork _unitOfWork;

		public TokensService(IConfiguration config, IUnitOfWork unitOfWork)
		{
			_config = config;
			_unitOfWork = unitOfWork;
		}

		public JwtTokens GenerateTokens(User user)
		{
			var accessToken = GenerateAccessToken(user);
			var refreshToken = GenerateRefreshToken();

			return new JwtTokens
			{
				AccessToken = accessToken,
				RefreshToken = refreshToken
			};
		}

		private string GenerateAccessToken(User user)
		{
			var jwtAuthenticationSecret = _config.GetConnectionString("JwtAuthenticationSecret");
			var jwtBytesFromAS = Encoding.UTF8.GetBytes(jwtAuthenticationSecret);
			var jwtSymmetricSecurityKey = new SymmetricSecurityKey(jwtBytesFromAS);
			var rolesNames = GetUserRolesNames(user.Id);
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(ClaimTypes.Email, user.Email)
			};
			claims.AddRange(rolesNames.Select(r => new Claim(ClaimTypes.Role, r)));
			var credentials = new SigningCredentials(jwtSymmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddHours(1),
				SigningCredentials = credentials
			};
			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}

		private IEnumerable<string> GetUserRolesNames(Guid userId)
		{
			var rolesIds = _unitOfWork.UserRole
				.FindBy(ur => ur.UserId == userId)
				.Select(ur => ur.RoleId);
			var rolesNames = new List<string>();

			foreach (var roleId in rolesIds)
			{
				var role = _unitOfWork.Role
					.FindBy(r => r.Id == roleId)
					.SingleOrDefault();

				if (role == null)
					throw new ArgumentNullException($"Couldn't find role with id {roleId}");
				rolesNames.Add(role.Name);
			}
			return rolesNames;
		}

		private static string GenerateRefreshToken()
		{
			var randomNumber = new byte[32];
			using var rng = RandomNumberGenerator.Create();

			rng.GetBytes(randomNumber);
			return Convert.ToBase64String(randomNumber);
		}
	}
}
