using System;
using System.Collections.Generic;

namespace GCommon.DTypes
{
	/// <summary>An experimental data type similar to a <see cref="bool"/> but with a third possible value meaning "no value"</summary>
	public class GTrinaryBoolean : IComparable, IEquatable<bool>, IComparable<bool>, IEquatable<GTrinaryBoolean>
	{
		public bool ValueExists { get; set; } = false;
		public bool ValueData { get; set; } = false;

		public GTrinaryBoolean() { }

		public GTrinaryBoolean(bool valueData)
		{
			ValueData = valueData;
			ValueExists = true;
		}

		public GTrinaryBoolean(bool valueData, bool valueExists) : this(valueExists) => ValueData = valueData;

		public bool IsTrueBool => ValueExists ? ValueData : false;
		public int IsTrueInt => ValueExists ? ValueData ? 1 : 2 : 0;

		/// <summary>Allows setting the object value based on an whether it exists or not. Can also be used to make the value non-existent.</summary>
		public bool this[bool exists]
		{
			get => exists == ValueExists ? ValueData : false;
			set
			{
				ValueExists = exists;
				ValueData = exists ? value : false;
			}
		}

		public TypeCode GetTypeCode() => TypeCode.Boolean;
		public bool GetStates(out bool state) => (state = ValueExists) ? ValueData : false;

		public override bool Equals(object obj) => base.Equals(obj);
		public bool Equals(bool other) => ValueData.Equals(other);

		public int CompareTo(bool other) => ValueData.CompareTo(other);
		public int CompareTo(object obj) => ValueData.CompareTo(obj);

		public bool Equals(GTrinaryBoolean other) => other != null && ValueData == other.ValueData;
		public override int GetHashCode() => 855104778 + ValueData.GetHashCode();

		public static bool operator ==(GTrinaryBoolean trinary1, GTrinaryBoolean trinary2) => EqualityComparer<GTrinaryBoolean>.Default.Equals(trinary1, trinary2);
		public static bool operator !=(GTrinaryBoolean trinary1, GTrinaryBoolean trinary2) => !(trinary1 == trinary2);
	}
}