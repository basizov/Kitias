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
		/// Take only one subject from db
		/// </summary>
		/// <param name="id">Id of the subject to take</param>
		/// <returns>Subject</returns>
		Task<Result<SubjectDto>> TakeSubjectByIdAsync(Guid id);
		/// <summary>
		/// Create subject
		/// </summary>
		/// <param name="subject">Model to create a subject</param>
		/// <returns>New subject</returns>
		Task<Result<SubjectDto>> CreateSubjectAsync(CreateSubjectModel subject);
		/// <summary>
		/// Update subject by id
		/// </summary>
		/// <param name="id">Id of the subject</param>
		/// <param name="Subject">Subject update model</param>
		/// <returns>Subject</returns>
		Task<Result<SubjectDto>> UpdateSubjectAsync(Guid id, UpdateSubjectModel Subject);
		/// <summary>
		/// Delete subject from db
		/// </summary>
		/// <param name="id">Id of the subject</param>
		/// <returns>Status message</returns>
		Task<Result<string>> DeleteSubjectAsync(Guid id);
	}
}
