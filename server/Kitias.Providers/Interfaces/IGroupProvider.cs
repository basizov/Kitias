using Kitias.Persistence.DTOs;
using Kitias.Providers.Models;
using Kitias.Providers.Models.Group;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kitias.Providers.Interfaces
{
	/// <summary>
	/// Provider to work with group
	/// </summary>
	public interface IGroupProvider
	{
		/// <summary>
		/// Take all groups from db
		/// </summary>
		/// <returns>Groups</returns>
		Result<IEnumerable<GroupDto>> TakeGroups();
		/// <summary>
		/// Take only one group from db
		/// </summary>
		/// <param name="id">Id of the group to take</param>
		/// <returns>Group</returns>
		Task<Result<GroupDto>> TakeGroupByIdAsync(Guid id);
		/// <summary>
		/// Create group
		/// </summary>
		/// <param name="group">Model to create a group</param>
		/// <returns>New group</returns>
		Task<Result<GroupDto>> CreateGroupAsync(CreateGroupModel group);
		/// <summary>
		/// Update group by id
		/// </summary>
		/// <param name="id">Id of the group</param>
		/// <param name="group">Group update model</param>
		/// <returns></returns>
		Task<Result<GroupDto>> UpdateGroupAsync(Guid id, UpdateGroupModel group);
		/// <summary>
		/// Delete group from db
		/// </summary>
		/// <param name="id">Id of the group</param>
		/// <returns>Status message</returns>
		Task<Result<string>> DeleteGroupAsync(Guid id);
	}
}
