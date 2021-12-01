namespace Kitias.Providers.Models.Subject
{
	/// <summary>
	/// Update subjects with name
	/// </summary>
	public record UpdateSubjectByName
	{
		/// <summary>
		/// Name for subjects
		/// </summary>
		public string Name { get; init; }
		/// <summary>
		/// New name for subjects
		/// </summary>
		public string NewName { get; init; }
	}
}
