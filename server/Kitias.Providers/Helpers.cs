using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Kitias.Providers
{
	/// <summary>
	/// Helpers to work with transmiters
	/// </summary>
	public static class Helpers
	{
		/// <summary>
		/// Get sting from enum member
		/// </summary>
		/// <typeparam name="T">Enum type</typeparam>
		/// <param name="payload">Enum type member</param>
		/// <returns>To string type</returns>
		public static string GetEnumMemberAttrValue<T>(T payload)
			where T : Enum
		{
			var type = typeof(T);
			var membersInfo = type.GetMember(payload.ToString());
			var memberInfo = membersInfo.SingleOrDefault();

			if (memberInfo != null)
			{
				var attributes = memberInfo.GetCustomAttributes(false)
					.OfType<EnumMemberAttribute>();
				var attribute = attributes.FirstOrDefault();

				if (attribute != null)
					return attribute.Value;
			}
			throw new ArgumentException("Couldn't find memberInfo");
		}

		/// <summary>
		/// Get enum member from string
		/// </summary>
		/// <typeparam name="T">Enum type</typeparam>
		/// <param name="payload">From which string</param>
		/// <returns>Enum type member</returns>
		public static T GetEnumMemberFromString<T>(string payload)
			where T : Enum
		{
			var type = typeof(T);

			foreach (var name in Enum.GetNames(type))
			{
				var fieldInfo = type.GetField(name);
				var fieldAttributes = (EnumMemberAttribute[])fieldInfo.GetCustomAttributes(typeof(EnumMemberAttribute), true);
				var fieldAttribute = fieldAttributes.SingleOrDefault();

				if (fieldAttribute != null && fieldAttribute.Value == payload)
					return (T)Enum.Parse(type, name);
			}
			throw new ArgumentException("Couldn't find fieldAttribute");
		}

		/// <summary>
		/// Get text file size
		/// </summary>
		/// <param name="size">Numeric size</param>
		/// <returns>Normal size</returns>
		public static string GetFileSizeFromNumber(long size)
		{
			var infoAmounts = new List<string> { "Byte", "KB", "MB", "GB", "TB", "PB", "EB" };

			if (size == 0)
				return $"0{infoAmounts[0]}";
			var bytes = Math.Abs(size);
			var place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
			var num = Math.Round(bytes / Math.Pow(1024, place), 1);
			var result = $"{Math.Sign(size) * num}{infoAmounts[place]}";

			return result;
		}
	}
}
