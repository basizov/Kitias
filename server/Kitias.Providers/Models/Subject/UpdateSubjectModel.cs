﻿using System;

namespace Kitias.Providers.Models.Subject
{
	/// <summary>
	/// Model to update subject
	/// </summary>
	public record UpdateSubjectModel
	{
		/// <summary>
		/// Subject name
		/// </summary>
		public string Name { get; init; }
		/// <summary>
		/// Subject type
		/// </summary>
		public string Type { get; init; }
		/// <summary>
		/// Subject time
		/// </summary>
		public string Time { get; init; }
		/// <summary>
		/// Subject date
		/// </summary>
		public string Date { get; init; }
		/// <summary>
		/// Subject week
		/// </summary>
		public string Week { get; init; }
		/// <summary>
		/// Subject day
		/// </summary>
		public string Day { get; init; }
		/// <summary>
		/// Subject theme
		/// </summary>
		public string Theme { get; init; }
		/// <summary>
		/// Flag to give scores
		/// </summary>
		public bool IsGiveScore { get; set; } = true;
	}
}
