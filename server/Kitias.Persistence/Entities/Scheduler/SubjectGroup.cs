using System;

namespace Kitias.Persistence.Entities.Scheduler
{
	/// <summary>
	/// Entity to concat to entities
	/// </summary>
	public record SubjectGroup
	{
		/// <summary>
		/// SubjectGroup identifier
		/// </summary>
		public Guid Id { get; init; }
		/// <summary>
		/// Subject group id
		/// </summary>
		public Guid GroupId { get; init; }
		/// <summary>
		/// Subject group
		/// </summary>
		public virtual Group Group { get; set; }
		/// <summary>
		/// Group subject id
		/// </summary>
		public Guid SubjectId { get; init; }
		/// <summary>
		/// Group subject
		/// </summary>
		public virtual Subject Subject { get; set; }
	}
}
