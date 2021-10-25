using System;
using System.Collections.Generic;

namespace Kitias.Identity.Server.Models.RequestModels
{
	public record RegisterRequestModel
	{
		public string UserName { get; init; }
		public string Email { get; init; }
		public string Password { get; init; }
		public IEnumerable<Guid> RolesIds { get; init; }
	}
}
