using System;

namespace Kitias.Auth.API.Models
{
	public class AuthorizationResultModel
	{
		public Guid Id { get; init; }
		public string Email { get; init; }
		public string FullName { get; init; }
		public string AccessToken { get; init; }
	}
}
