namespace Kitias.Providers.Models.Subject
{
	/// <summary>
	/// Model to delete subjects by name
	/// </summary>
	public record DeleteSubjectByNameModel
	{
		/// <summary>
		/// Names of subjects
		/// </summary>
		public string Name { get; init; }
	}
}
