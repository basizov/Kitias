using System;
using System.Collections.Generic;

namespace Kitias.Providers.Models.Attendances
{
	/// <summary>
	/// Model to take sheduler students and group
	/// </summary>
	public record ShedulerStudentsGroup
	{
		/// <summary>
		/// Sheduler group id
		/// </summary>
		public Guid? GroupId { get; init; }
		/// <summary>
		/// Sheduler students
		/// </summary>
		public IEnumerable<string> Students { get; init; }
	}
}
