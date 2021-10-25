using Kitias.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kitias.API.Middlewares
{
	public class ErrorHandlerMiddleware
	{
		private readonly ILogger _logger;
		private readonly RequestDelegate _next;

		public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger, RequestDelegate next)
		{
			_logger = logger;
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				var response = new ExceptionModel
				{
					StatusCode = (int)HttpStatusCode.InternalServerError,
					Message = ex.Message,
					From = ex.StackTrace
				};

				await context.Response.WriteAsync(JsonSerializer.Serialize(response));
			}
		}
	}
}
