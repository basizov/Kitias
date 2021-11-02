using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Kitias.API.Middlewares
{
	/// <summary>
	/// 
	/// </summary>
	public class AuthenticationMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<AuthenticationMiddleware> _logger;

		/// <summary>
		/// Take neccasary services
		/// </summary>
		/// <param name="next">Next action</param>
		/// <param name="logger">Logginh</param>
		public AuthenticationMiddleware(RequestDelegate next, ILogger<AuthenticationMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		/// <summary>
		/// Use middleware
		/// </summary>
		/// <param name="context">Http context</param>
		/// <returns>Next pipeline</returns>
		public async Task InvokeAsync(HttpContext context)
		{
			var token = context.Request.Cookies[".AspNetCore.Application.Guid"];

			if (!string.IsNullOrEmpty(token))
			{
				_logger.LogInformation($"Got token from cookies: {token}");
				context.Request.Headers.Add("Authorization", $"Bearer {token}");
				_logger.LogInformation("Created neccessary headers for protection and autentication");
			}
			await _next(context);
		}
	}
}
