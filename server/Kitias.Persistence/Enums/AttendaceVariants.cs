using System.Runtime.Serialization;

namespace Kitias.Persistence.Enums
{
	/// <summary>
	/// Variants of attendance
	/// </summary>
	public enum AttendaceVariants
	{
		/// <summary>
		/// Student wasn't in the pair (Не было)
		/// </summary>
		[EnumMember(Value = "Н")]
		WasNot = 0,
		/// <summary>
		/// Student wasn't in the pair, because he was ill (Болел)
		/// </summary>
		[EnumMember(Value = "Б")]
		WasIll,
		/// <summary>
		/// Student wasn't in the pair by serious reason (Отсутствовал по уважительной причине)
		/// </summary>
		[EnumMember(Value = "О")]
		WasNotByReason,
		/// <summary>
		/// Student was in the pair (Присутствовал)
		/// </summary>
		[EnumMember(Value = "+")]
		Was
	}
}
