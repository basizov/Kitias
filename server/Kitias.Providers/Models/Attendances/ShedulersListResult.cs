using System;

namespace Kitias.Providers.Models.Attendances
{
	/// <summary>
	/// All shedulers of the teacher
	/// </summary>
	public record ShedulersListResult
	{
		/// <summary>
		/// Sheduler Identifier
		/// </summary>
		public Guid Id { get; init; }
		/// <summary>
		/// Sheduler name
		/// </summary>
		public string Name { get; init; }
		/// <summary>
		/// Number of the group
		/// </summary>
		public string GroupNumber { get; init; }
		/// <summary>
		/// Subject name
		/// </summary>
		public string SubjectName { get; init; }
	}
}
