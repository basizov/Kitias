using AutoMapper;
using Kitias.Repository.Interfaces.Base;
using Microsoft.Extensions.Logging;

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
	}
}
