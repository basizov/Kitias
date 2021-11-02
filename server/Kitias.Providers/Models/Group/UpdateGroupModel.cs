using System;
using System.Collections.Generic;

namespace Kitias.Providers.Models.Group
{
	public record UpdateGroupModel
	{
		public byte Course { get; init; }
		public string Number { get; init; }
		public IEnumerable<Guid> StudentsIds { get; init; }
	}
}
