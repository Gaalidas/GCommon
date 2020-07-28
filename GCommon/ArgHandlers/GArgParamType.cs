using System;
using System.Collections.Generic;
using GCommon.Enums;

namespace GCommon.ArgHandlers
{
	public class GArgParamType : IEquatable<GArgParamType>
	{
		public DataType ParamType { get; private set; }
		public GArgParamType(object input) => DetermineDataType(input);

		public void DetermineDataType(object input)
		{
			if (input.DetermineDataType(out DataType outType))
				ParamType = outType;
		}

		public override bool Equals(object obj) => Equals(obj as GArgParamType);
		public bool Equals(GArgParamType other) => other != null && ParamType == other.ParamType;
		public override int GetHashCode() => -2121073166 + ParamType.GetHashCode();

		public static bool operator ==(GArgParamType type1, GArgParamType type2) => EqualityComparer<GArgParamType>.Default.Equals(type1, type2);
		public static bool operator !=(GArgParamType type1, GArgParamType type2) => !(type1 == type2);
	}
}