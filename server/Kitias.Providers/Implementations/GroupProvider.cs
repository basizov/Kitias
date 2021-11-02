using AutoMapper;
using Kitias.Persistence.DTOs;
using Kitias.Providers.Interfaces;
using Kitias.Providers.Models;
using Kitias.Providers.Models.Group;
using Kitias.Repository.Interfaces.Base;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kitias.Providers.Implementations
{
	public class GroupProvider : Provider, IGroupProvider
	{
		public GroupProvider(IMapper mapper, ILogger<GroupProvider> logger, IUnitOfWork unitOfWork) : base(mapper, logger, unitOfWork) { }

		public Result<IEnumerable<GroupDto>> TakeGroups()
		{
			try
			{
				var groups = _unitOfWork.Group.GetAll();
				var result = _mapper.Map<IEnumerable<GroupDto>>(groups);

				
				return ResultHandler.OnSuccess(result);
			}
			catch (Exception ex)
			{
				return ResultHandler.OnFailure<IEnumerable<GroupDto>>(ex.Message);
			}
		}

		public Task<Result<GroupDto>> TakeGroupByIdAsync(Guid id) => throw new NotImplementedException();

		public Task<Result<GroupDto>> UpdateGroupAsync(Guid id, UpdateGroupModel group) => throw new NotImplementedException();

		public Task<Result<GroupDto>> CreateGroupAsync(CreateGroupModel group) => throw new NotImplementedException();

		public Task<Result<GroupDto>> DeleteGroupAsync(Guid id) => throw new NotImplementedException();
	}
}
