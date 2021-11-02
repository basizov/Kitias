using System;
using System.Collections.Generic;

namespace Kitias.Providers.Models.Group
{
	public record CreateGroupModel
	{
		public byte Course { get; init; }
		public string Number { get; init; }
		public string EducationType { get; init; }
		public string Speciality { get; init; }
		public string ReceiptDate { get; init; }
		public string IssueDate { get; init; }
		public IEnumerable<Guid> StudentsIds { get; init; }
	}
}
