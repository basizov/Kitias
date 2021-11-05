using System;
using System.Collections.Generic;

namespace Kitias.Providers.Models.Group
{
	/// <summary>
	/// Model to update the exsisted group
	/// </summary>
	public record UpdateGroupModel
	{
		/// <summary>
		/// New course of the group
		/// </summary>
		public byte? Course { get; init; }
		/// <summary>
		/// New group number
		/// </summary>
		public string? Number { get; init; }
		/// <summary>
		/// New students of the group
		/// </summary>
		public IEnumerable<Guid> StudentsIds { get; init; }
	}
}
