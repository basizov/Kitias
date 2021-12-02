using System.Runtime.Serialization;

namespace Kitias.Persistence.Enums
{
	/// <summary>
	/// Type of the week
	/// </summary>
	public enum Week
	{
		/// <summary>
		/// Everu week
		/// </summary>
		[EnumMember(Value = "Еженедельно")]
		Every = 0,
		/// <summary>
		/// Every 2 weeks
		/// </summary>
		[EnumMember(Value = "Каждые 2 недели")]
		Even = 1,
		/// <summary>
		/// Only by dates
		/// </summary>
		[EnumMember(Value = "По определенным данным")]
		ByDate = 2
	}
}
