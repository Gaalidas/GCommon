using System;
using System.Collections;
using System.Collections.Generic;
using GCommon.Enums;

namespace GCommon.Collections.Params
{
	public class GParamArray
	{
		public GParamType TypeOfParam { get; set; }
		public DataType GetDataType() => TypeOfParam.ParamType;
		public static GParamArray<T> Empty<T>() => new GParamArray<T>();
	}

	/// <summary>Supports multiple parameters of the same type in one object.</summary>
	public class GParamArray<T> : GParamArray, IEquatable<GParamArray<T>>, IEnumerable<T>
	{
		public GList<T> ItemList { get; set; } = new GList<T>();

		public int Count => ItemList.Count;
		public int LastIndex => ItemList.LastIndex;

		public GParamArray() { }

		public GParamArray(T[] itemArray)
		{
			ItemList = itemArray.ToGList();

			if (Count > 0)
				TypeOfParam.DetermineDataType(ItemList[0]);
		}

		public GParamArray(GList<T> itemList)
		{
			ItemList = itemList;

			if (Count > 0)
				TypeOfParam.DetermineDataType(ItemList[0]);
		}

		public void SetAll(T[] newArray)
		{
			ItemList = newArray.ToGList();

			if (Count > 0)
				TypeOfParam.DetermineDataType(ItemList[0]);
		}

		public override bool Equals(object obj) => Equals(obj as GParamArray<T>);
		public bool Equals(GParamArray<T> other) => other != null && EqualityComparer<GList<T>>.Default.Equals(ItemList, other.ItemList);
		public override int GetHashCode() => -301778430 + EqualityComparer<GList<T>>.Default.GetHashCode(ItemList);

		public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)ItemList).GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<T>)ItemList).GetEnumerator();

		public static bool operator ==(GParamArray<T> array1, GParamArray<T> array2) => EqualityComparer<GParamArray<T>>.Default.Equals(array1, array2);
		public static bool operator !=(GParamArray<T> array1, GParamArray<T> array2) => !(array1 == array2);
	}
}