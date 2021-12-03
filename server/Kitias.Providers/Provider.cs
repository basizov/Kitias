using AutoMapper;
using Kitias.Providers.Models;
using Kitias.Repository.Interfaces.Base;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Kitias.Providers
{
	/// <summary>
	/// Base provider
	/// </summary>
	public class Provider
	{
		protected readonly IMapper _mapper;
		protected readonly ILogger _logger;
		protected readonly IUnitOfWork _unitOfWork;

		/// <summary>
		/// Map neccassary services
		/// </summary>
		/// <param name="mapper">Mapper service</param>
		/// <param name="logger">Logging</param>
		/// <param name="unitOfWork">Take info about entities</param>
		public Provider(IMapper mapper, ILogger<Provider> logger, IUnitOfWork unitOfWork) =>
			(_mapper, _logger, _unitOfWork) = (mapper, logger, unitOfWork);

		protected async Task<Result<K>> TryCatchExecute<T, K>(T parameter, Func<T, Task<Result<K>>> executeFunc)
			where T : class
			where K : class
		{
			try
			{
				var result = await executeFunc(parameter);

				return result;

			}
			catch (ApplicationException ex)
			{
				throw;
			}
			catch (Exception ex)
			{
				return ReturnFailureResult<K>(ex.Message, $"Error {nameof(T)} data");
			}
		}

		/// <summary>
		/// Return failure resul with logging
		/// </summary>
		/// <typeparam name="T">Type of return value</typeparam>
		/// <param name="loggerMessage">Message for logging</param>
		/// <param name="errorMessage">Message for client</param>
		/// <returns>Failure result</returns>
		protected Result<T> ReturnFailureResult<T>(string loggerMessage, string errorMessage = null)
			where T : class
		{
			_logger.LogError(loggerMessage);
			return ResultHandler.OnFailure<T>(errorMessage ?? loggerMessage);
		}
	}
}
