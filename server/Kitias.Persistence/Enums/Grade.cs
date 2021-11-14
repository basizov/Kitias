using System.Runtime.Serialization;

namespace Kitias.Persistence.Enums
{
	/// <summary>
	/// Grade of the subject
	/// </summary>
	public enum Grade
	{
		/// <summary>
		/// Too low grade
		/// </summary>
		[EnumMember(Value = "Не допущен")]
		NotAdmitted = 0,
		/// <summary>
		/// Bad exam
		/// </summary>
		[EnumMember(Value = "Пересдача")]
		Retake = 1,
		/// <summary>
		/// Grader "3"
		/// </summary>
		[EnumMember(Value = "Удовлетворительно")]
		Satisfactorily = 2,
		/// <summary>
		/// Grader "4"
		/// </summary>
		[EnumMember(Value = "Хорошо")]
		Good = 3,
		/// <summary>
		/// Grader "5"
		/// </summary>
		[EnumMember(Value = "Отлично")]
		Excellent = 4
	}
}
