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
		[EnumMember(Value = GradeNames.NOTADMITTED_GRADE)]
		NotAdmitted = 0,
		/// <summary>
		/// Bad exam
		/// </summary>
		[EnumMember(Value = GradeNames.RETAKE_GRADE)]
		Retake = 1,
		/// <summary>
		/// Grader "3"
		/// </summary>
		[EnumMember(Value = GradeNames.SATISFACTORILY_GRADE)]
		Satisfactorily = 2,
		/// <summary>
		/// Grader "4"
		/// </summary>
		[EnumMember(Value = GradeNames.GOOD_GRADE)]
		Good = 3,
		/// <summary>
		/// Grader "5"
		/// </summary>
		[EnumMember(Value = GradeNames.EXCELLENT_GRADE)]
		Excellent = 4,
		/// <summary>
		/// Start of the session
		/// </summary>
		[EnumMember(Value = GradeNames.NONE_GRADE)]
		None = 5
	}

	/// <summary>
	/// Class which stored all grade names
	/// </summary>
	public static class GradeNames
	{
		/// <summary>
		/// None grade value
		/// </summary>
		public const string NONE_GRADE = "-";
		/// <summary>
		/// summary grade value
		/// </summary>
		public const string EXCELLENT_GRADE= "Отлично";
		/// <summary>
		/// Good grade value
		/// </summary>
		public const string GOOD_GRADE = "Хорошо";
		/// <summary>
		/// Satisfactorily grade value
		/// </summary>
		public const string SATISFACTORILY_GRADE = "Удовлетворительно";
		/// <summary>
		/// Bad grade value
		/// </summary>
		public const string RETAKE_GRADE = "Пересдача";
		/// <summary>
		/// Too low grade value
		/// </summary>
		public const string NOTADMITTED_GRADE = "Не допущен";
	}
}
