using Kitias.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kitias.API.Middlewares
{
	/// <summary>
	/// Middleware for catch errors
	/// </summary>
	public class ErrorHandlerMiddleware
	{
		private readonly ILogger _logger;
		private readonly RequestDelegate _next;

		/// <summary>
		/// Take neccasary services
		/// </summary>
		/// <param name="logger">Logging</param>
		/// <param name="next">Next action</param>
		public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger, RequestDelegate next)
		{
			_logger = logger;
			_next = next;
		}

		/// <summary>
		/// Use middleware
		/// </summary>
		/// <param name="context">Http context</param>
		/// <returns>Next piplene</returns>
		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				context.Response.ContentType = "application/json";
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				var response = new ExceptionModel
				{
					StatusCode = context.Response.StatusCode,
					Message = ex.Message,
					From = ex.StackTrace
				};

				await context.Response.WriteAsync(JsonSerializer.Serialize(response));
			}
		}
	}
}
