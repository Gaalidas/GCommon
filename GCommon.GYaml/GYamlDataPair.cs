using System;
using System.Collections.Generic;

namespace GCommon.GYaml
{
	public class GYamlDataPair : IEquatable<GYamlDataPair>
	{
		#region Fields
		public string Key { get; private set; }
		public string Value { get; set; }
		#endregion

		#region Validation
		public bool IsValid => Key != null && Value != null && !Key.IsNullOrEmpty() && !Value.IsNullOrEmpty();
		#endregion

		#region Constructors
		public GYamlDataPair() { }
		public GYamlDataPair(string key) => Key = key;
		public GYamlDataPair(string key, string value) : this(key) => Value = value;

		public GYamlDataPair(string[] pairStr)
		{
			if (pairStr.Length == 2)
			{
				Key = pairStr[0];
				Value = pairStr[1];
			}
		}

		public string this[string key]
		{
			get => Key.ToLower() == key.ToLower() ? Value : string.Empty;
			set
			{
				if (Key.ToLower() == key.ToLower())
					Value = value;
			}
		}
		#endregion

		#region Overrides
		public override string ToString() => $"{Key}: {Value}";
		public override bool Equals(object obj) => Equals(obj as GYamlDataPair);

		public override int GetHashCode()
		{
			int hashCode = 206514262;
			hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Key);
			hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Value);
			return hashCode;
		}
		#endregion

		#region Equality
		public bool Equals(GYamlDataPair other) => other != null && Key == other.Key && Value == other.Value;
		public static bool operator ==(GYamlDataPair pair1, GYamlDataPair pair2) => EqualityComparer<GYamlDataPair>.Default.Equals(pair1, pair2);
		public static bool operator !=(GYamlDataPair pair1, GYamlDataPair pair2) => !(pair1 == pair2);
		#endregion
	}
}