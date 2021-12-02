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
		/// Take all groups names from db
		/// </summary>
		/// <returns>Groups names</returns>
		Result<IEnumerable<GroupNames>> TakeGroupsNames();
		/// <summary>
		/// Take all students of the group from db
		/// </summary>
		/// <param name="id">Id of the group to take</param>
		/// <returns>Groups</returns>
		Task<Result<IEnumerable<StudentDto>>> TakeGroupStudentsAsync(Guid id);
		/// <summary>
		/// Take all students of the group from db
		/// </summary>
		/// <param name="id">Id of the group to take</param>
		/// <returns>Groups</returns>
		Task<Result<IEnumerable<string>>> TakeGroupStudentsNamesAsync(Guid id);
		/// <summary>
		/// Take all subjects of the group from db
		/// </summary>
		/// <param name="id">Id of the group to take</param>
		/// <returns>Subjects</returns>
		Task<Result<IEnumerable<SubjectDto>>> TakeGroupSubjectsAsync(Guid id);
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
		/// Create group students
		/// </summary>
		/// <param name="id">Id of the group to take</param>
		/// <param name="students">New students to the group</param>
		/// <returns>Students</returns>
		Task<Result<IEnumerable<StudentDto>>> CreateGroupStudentsAsync(Guid id, IEnumerable<Guid> students);
		/// <summary>
		/// Create group subjects
		/// </summary>
		/// <param name="id">Id of the group to take</param>
		/// <param name="subjects">New subjects to the group</param>
		/// <returns>Subjects</returns>
		Task<Result<IEnumerable<SubjectDto>>> CreateGroupSubjectsAsync(Guid id, IEnumerable<Guid> subjects);
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
		/// <summary>
		/// Delete ubjects from the group
		/// </summary>
		/// <param name="id">Id of the group</param>
		/// <param name="subjects">Subjects to delete from the group</param>
		/// <returns>Status message</returns>
		Task<Result<string>> DeleteGroupSubjectsAsync(Guid id, IEnumerable<Guid> subjects);
	}
}
