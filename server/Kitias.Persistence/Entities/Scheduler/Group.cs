using Kitias.Persistence.Entities.People;
using Kitias.Persistence.Enums;
using System;
using System.Collections.Generic;

namespace Kitias.Persistence.Entities.Scheduler
{
	/// <summary>
	/// Group model
	/// </summary>
	public record Group
	{
		/// <summary>
		/// Group identifier
		/// </summary>
		public Guid Id { get; init; }
		/// <summary>
		/// Group course
		/// </summary>
		public byte Course { get; set; }
		/// <summary>
		/// Group number
		/// </summary>
		public string Number { get; set; }
		/// <summary>
		/// Group education type
		/// </summary>
		public EducationType EducationType { get; init; }
		/// <summary>
		/// Group speciality
		/// </summary>
		public Speciality Speciality { get; init; }
		/// <summary>
		/// Group receipt date
		/// </summary>
		public DateTime ReceiptDate { get; init; }
		/// <summary>
		/// Group issue date
		/// </summary>
		public DateTime IssueDate { get; init; }
		/// <summary>
		/// Group students
		/// </summary>
		public virtual ICollection<Student> Students { get; set; }
		/// <summary>
		/// Group subjects
		/// </summary>
		public virtual ICollection<SubjectGroup> Groups { get; init; }
	}
}
