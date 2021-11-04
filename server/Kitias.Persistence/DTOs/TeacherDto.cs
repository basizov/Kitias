using System;

namespace Kitias.Persistence.DTOs
{
	/// <summary>
	/// Teacher data transfer object
	/// </summary>
	public record TeacherDto
	{
		/// <summary>
		/// Teacher identifier
		/// </summary>
		public Guid Id { get; init; }
		/// <summary>
		/// Teacher e-mail
		/// </summary>
		public string Email { get; init; }
		/// <summary>
		/// Teacher FIO
		/// </summary>
		public string FullName { get; init; }
	}
}
