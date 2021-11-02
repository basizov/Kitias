using AutoMapper;
using Kitias.Repository.Interfaces.Base;
using Microsoft.Extensions.Logging;

namespace Kitias.Providers
{
	public class Provider
	{
		protected readonly IMapper _mapper;
		protected readonly ILogger _logger;
		protected readonly IUnitOfWork _unitOfWork;

		public Provider(IMapper mapper, ILogger<Provider> logger, IUnitOfWork unitOfWork) =>
			(_mapper, _logger, _unitOfWork) = (mapper, logger, unitOfWork);
	}
}
