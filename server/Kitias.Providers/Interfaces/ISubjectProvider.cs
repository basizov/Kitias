using Kitias.Persistence.DTOs;
using Kitias.Providers.Models;
using Kitias.Providers.Models.Subject;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kitias.Providers.Interfaces
{
	/// <summary>
	/// Provider to work with subject db
	/// </summary>
	public interface ISubjectProvider
	{
		/// <summary>
		/// Take all subjects from db
		/// </summary>
		/// <returns>Subjects</returns>
		Result<IEnumerable<SubjectDto>> TakeSubjects();
		/// <summary>
		/// Take subject sheduler from db
		/// </summary>
		/// <param name="name">Name of the subject to take</param>
		/// <returns>Subjects</returns>
		Task<Result<AttendanceShedulerDto>> TakeSubjectShedulerAsync(string name);
		/// <summary>
		/// Take only one subject from db
		/// </summary>
		/// <param name="id">Id of the subject to take</param>
		/// <returns>Subject</returns>
		Task<Result<SubjectDto>> TakeSubjectByIdAsync(Guid id);
		/// <summary>
		/// Take all groups of the subject
		/// </summary>
		/// <param name="id">Id of the subject to take</param>
		/// <returns>Groups</returns>
		Task<Result<IEnumerable<GroupDto>>> TakeSubjectGroupsAsync(Guid id);
		/// <summary>
		/// Create subject
		/// </summary>
		/// <param name="subject">Model to create a subject</param>
		/// <param name="email">Teacher email</param>
		/// <returns>New subject</returns>
		Task<Result<IEnumerable<SubjectDto>>> CreateSubjectAsync(IEnumerable<CreateSubjectModel> subjects, string email);
		/// <summary>
		/// Create subject groups
		/// </summary>
		/// <param name="id">Id of the subject to take</param>
		/// <param name="groups">New groups to the subject</param>
		/// <returns>Groups</returns>
		Task<Result<IEnumerable<GroupDto>>> CreateSubjectGroupsAsync(Guid id, IEnumerable<Guid> groups);
		/// <summary>
		/// Update subject by id
		/// </summary>
		/// <param name="id">Id of the subject</param>
		/// <param name="Subject">Subject update model</param>
		/// <returns>Subject</returns>
		Task<Result<SubjectDto>> UpdateSubjectAsync(Guid id, UpdateSubjectModel Subject);
		/// <summary>
		/// Update subjects by name
		/// </summary>
		/// <param name="name">Name of the subjects</param>
		/// <param name="newName">New name for the subjects</param>
		/// <returns>Subjects</returns>
		Task<Result<IEnumerable<SubjectDto>>> UpdateSubjectsByNameAsync(string name, string newName);
		/// <summary>
		/// Delete subjects by name
		/// </summary>
		/// <param name="name">Name of the subjects</param>
		/// <returns>Status message</returns>
		Task<Result<string>> DeleteSubjectsByNameAsync(string name);
		/// <summary>
		/// Delete subject from db
		/// </summary>
		/// <param name="id">Id of the subject</param>
		/// <returns>Status message</returns>
		Task<Result<string>> DeleteSubjectAsync(Guid id);
		/// <summary>
		/// Delete groups from the subject
		/// </summary>
		/// <param name="id">Id of the subject</param>
		/// <param name="groups">Groups to delete from the subject</param>
		/// <returns>Groups message</returns>
		Task<Result<string>> DeleteSubjectGroupsAsync(Guid id, IEnumerable<Guid> groups);
	}
}
