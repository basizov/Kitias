using AutoMapper;
using Kitias.Repository.Interfaces.Base;
using Microsoft.Extensions.Logging;

namespace Kitias.Providers
{
	public class Provider
	{
		private readonly IMapper _mapper;
		private readonly ILogger _logger;
		private readonly IUnitOfWork _unitOfWork;

		public Provider(IMapper mapper, IUnitOfWork unitOfWork, ILogger logger)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
			_logger = logger;
		}
	}
}
