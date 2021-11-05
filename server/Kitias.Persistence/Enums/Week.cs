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
		/// Only even weeks
		/// </summary>
		[EnumMember(Value = "Четная неделя")]
		Even = 1,
		/// <summary>
		/// Only odd weeks
		/// </summary>
		[EnumMember(Value = "Нечетная неделя")]
		Odd = 2
	}
}
