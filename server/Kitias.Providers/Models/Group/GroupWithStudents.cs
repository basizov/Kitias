using System;
using System.Collections.Generic;

namespace Kitias.Providers.Models.Group
{
	/// <summary>
	/// Group data with sstudent transfer object
	/// </summary>
	public record GroupWithStudents
	{
		/// <summary>
		/// Group identifier
		/// </summary>
		public Guid Id { get; init; }
		/// <summary>
		/// Group course
		/// </summary>
		public byte Course { get; init; }
		/// <summary>
		/// Group number
		/// </summary>
		public string Number { get; init; }
		/// <summary>
		/// Group students
		/// </summary>
		public IEnumerable<StudentInGroup> Students { get; init; }
	}

	/// <summary>
	/// Student model in group
	/// </summary>
	public record StudentInGroup
	{
		/// <summary>
		/// Student identifier
		/// </summary>
		public Guid Id { get; init; }
		/// <summary>
		/// Student FIO
		/// </summary>
		public string FullName { get; init; }
	}
}
