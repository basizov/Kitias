using System;

namespace Kitias.Providers.Models.Person
{
	/// <summary>
	/// Teacher sheduler model
	/// </summary>
	public record TeacherShedulerModel
	{
		/// <summary>
		/// Teacher identifier
		/// </summary>
		public Guid Id { get; init; }
		/// <summary>
		/// Subject name
		/// </summary>
		public string SubjectName { get; init; }
		/// <summary>
		/// Sheduler name
		/// </summary>
		public string Name { get; init; }
		/// <summary>
		/// Teacher FIO
		/// </summary>
		public string FullName { get; init; }
	}
}
