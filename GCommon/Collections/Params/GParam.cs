using System;
using System.Collections.Generic;

namespace GCommon.Collections.Params
{
	public class GParam<T> : IEquatable<GParam<T>>
	{
		public T Param { get; set; }

		public GParam() { }
		public GParam(T item) => Param = item;

		public override bool Equals(object obj) => Equals(obj as GParam<T>);
		public bool Equals(GParam<T> other) => other != null && EqualityComparer<T>.Default.Equals(Param, other.Param);
		public override int GetHashCode() => -1510660958 + EqualityComparer<T>.Default.GetHashCode(Param);

		public static bool operator ==(GParam<T> param1, GParam<T> param2) => EqualityComparer<GParam<T>>.Default.Equals(param1, param2);
		public static bool operator !=(GParam<T> param1, GParam<T> param2) => !(param1 == param2);
	}
}