using Kitias.API.Models;
using Kitias.Persistence.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Kitias.API.Services
{
	public class TokensService
	{
		private readonly IConfiguration _config;

		public TokensService(IConfiguration config) => _config = config;

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
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(ClaimTypes.Email, user.Email)
			};
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

		private string GenerateRefreshToken()
		{
			var randomNumber = new byte[32];
			using var rng = RandomNumberGenerator.Create();

			rng.GetBytes(randomNumber);
			return Convert.ToBase64String(randomNumber);
		}
	}
}
