using System;
using System.Collections.Generic;
using GCommon.Enums;

namespace GCommon.Collections.Params
{
	/// <summary>A way around the limitation of a single type in a list for the purpose of enumeration values.</summary>
	public class GParamType : IEquatable<GParamType>
	{
		public DataType ParamType { get; private set; }

		public GParamType(object input) => DetermineDataType(input);

		public void DetermineDataType(object input) => ParamType = input.DetermineDataType(out DataType outType) ? outType : DataType.Unknown;

		public override bool Equals(object obj) => Equals(obj as GParamType);
		public bool Equals(GParamType other) => other != null && ParamType == other.ParamType;
		public override int GetHashCode() => -2121073166 + ParamType.GetHashCode();

		public static bool operator ==(GParamType type1, GParamType type2) => EqualityComparer<GParamType>.Default.Equals(type1, type2);
		public static bool operator !=(GParamType type1, GParamType type2) => !(type1 == type2);
	}
}