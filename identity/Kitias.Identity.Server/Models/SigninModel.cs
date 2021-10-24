using System;
using System.Collections.Generic;

namespace Kitias.Identity.Server.Models
{
	public record SigninModel
	{
		public string UserName { get; init; }
		public string Password { get; init; }
		public string ClientId { get; init; }
		public string ClientSecret { get; init; }
	}
}
