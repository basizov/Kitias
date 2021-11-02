using Kitias.Persistence.DTOs;
using Kitias.Providers.Models;
using Kitias.Providers.Models.Group;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kitias.Providers.Interfaces
{
	public interface IGroupProvider
	{
		Result<IEnumerable<GroupDto>> TakeGroups();
		Task<Result<GroupDto>> TakeGroupByIdAsync(Guid id);
		Task<Result<GroupDto>> CreateGroupAsync(CreateGroupModel group);
		Task<Result<GroupDto>> UpdateGroupAsync(Guid id, UpdateGroupModel group);
		Task<Result<GroupDto>> DeleteGroupAsync(Guid id);
	}
}
