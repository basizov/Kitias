using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Kitias.API.Middlewares
{
	public class AuthenticationMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<AuthenticationMiddleware> _logger;

		public AuthenticationMiddleware(RequestDelegate next, ILogger<AuthenticationMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			var token = context.Request.Cookies[".AspNetCore.Application.Guid"];

			if (!string.IsNullOrEmpty(token))
			{
				_logger.LogInformation($"Got token from cookies: {token}");
				context.Request.Headers.Add("Authorization", "Bearer " + token);
				context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
				context.Response.Headers.Add("X-Xss-Protection", "1");
				context.Response.Headers.Add("X-Frame-Options", "DENY");
				_logger.LogInformation("Created neccessary headers for protection and autentication");
			}
			await _next(context);
		}
	}
}
