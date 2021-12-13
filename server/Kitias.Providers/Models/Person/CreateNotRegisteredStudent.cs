namespace Kitias.Providers.Models.Person
{
	/// <summary>
	/// Model to create no authorized student
	/// </summary>
	public record CreateNotRegisteredStudent
	{
		/// <summary>
		/// Student full name
		/// </summary>
		public string FullName { get; init; }
		/// <summary>
		/// Student group number
		/// </summary>
		public string GroupNumber { get; init; }
	}
}
