using System;
using System.Collections.Generic;

namespace GCommon.ArgHandlers
{
	public class GArgData { }

	public class GArgData<T> : GArgData, IEquatable<GArgData<T>>
	{
		public GArgTag Parent { get; set; }
		public GArgParamSet<T> Data { get; set; } = default;

		public GArgData(GArgTag parent) => Parent = parent;
		public GArgData(GArgTag parent, T data) : this(parent) => Data.Item1 = data;

		public override bool Equals(object obj) => Equals(obj as GArgData<T>);
		public bool Equals(GArgData<T> other) => other != null && EqualityComparer<GArgParamSet<T>>.Default.Equals(Data, other.Data);
		public override int GetHashCode() => -301143667 + EqualityComparer<GArgParamSet<T>>.Default.GetHashCode(Data);

		public static bool operator ==(GArgData<T> data1, GArgData<T> data2) => EqualityComparer<GArgData<T>>.Default.Equals(data1, data2);
		public static bool operator !=(GArgData<T> data1, GArgData<T> data2) => !(data1 == data2);
	}
}