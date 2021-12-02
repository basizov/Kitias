using System;

namespace Kitias.Providers.Models.Group
{
	/// <summary>
	/// Group names transfer object
	/// </summary>
	public record GroupNames
	{
		/// <summary>
		/// Group identifier
		/// </summary>
		public Guid Id { get; init; }
		/// <summary>
		/// Group number
		/// </summary>
		public string Number { get; init; }
	}
}
