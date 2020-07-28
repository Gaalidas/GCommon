using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GCommon.Collections
{
	/// <summary>A wrapper for the standard <see cref="Array"/> class with extra features enabled to being it more in line with the Collections library.</summary>
	public class GArray
	{
		public GArray<T> Empty<T>() => new GArray<T>();
	}

	public class GArray<T> : GArray, IEnumerable<T>, IEquatable<GArray<T>>
	{
		public T[] Items { get; set; } = Array.Empty<T>();

		public GArray() { }
		public GArray(int size) => Items = new T[size];
		public GArray(Array input) => Items = input.Cast<T>().ToArray();

		public int Count => Items.Length;
		public int LastIndex => Count - 1;
		public bool IsEmpty => Count == 0;

		public T this[int index]
		{
			get => Items[index];
			set
			{
				if (index <= LastIndex)
					Items[index] = value;
			}
		}

		public void ForEach(Action<T> action)
		{
			if (action == null)
				return;

			for (int i = 0; i < Count; i++)
				action(Items[i]);
		}

		public bool Contains(T item)
		{
			for (int i = 0; i < Count; i++)
			{
				if ((object)Items[i] == (object)item)
					return true;
			}

			return false;
		}

		public bool TrueForAll()
		{
			if (Items is bool[] outBools)
			{
				foreach (bool bitem in outBools)
				{
					if (!bitem)
						return false;
				}

				return true;
			}
			else
			{
				for (int i = 0; i < Count; i++)
				{
					if (Items[i] == default)
						return false;
				}

				return true;
			}
		}

		public void Clear() => Items = Array.Empty<T>();

		public void Add(T item)
		{
			GList<T> tempList = Items.ToGList();
			tempList.Add(item);
			Items = tempList.ToArray();
		}

		public void AddRange(T[] items)
		{
			GList<T> tempList = Items.ToGList();

			for (int i = 0; i < items.Length; i++)
				tempList.Add(items[i]);

			Items = tempList.ToArray();
		}

		public void Remove(T item)
		{
			GList<T> tempArray = Items.ToGList();

			foreach (T titem in tempArray)
			{
				if ((object)titem == (object)item)
					tempArray.Remove(item);
			}

			Items = tempArray.ToArray();
		}

		public void RemoveAt(int index)
		{
			GList<T> tempList = Items.ToGList();

			if (index <= tempList.Count)
				tempList.RemoveAt(index);

			Items = tempList.ToArray();
		}

		public void RemoveRange(int index, int count)
		{
			GList<T> tempList = Items.ToGList();
			tempList.RemoveRange(index, count);
			Items = tempList.ToArray();
		}

		public void RemoveIdenticalTo(T[] items)
		{
			GList<T> tempList = Items.ToGList();

			foreach (T titem in tempList)
			{
				for (int i = 0; i < items.Length; i++)
				{
					if ((object)titem == (object)items[i])
						tempList.RemoveAt(tempList.IndexOf(titem));
				}
			}

			Items = tempList.ToArray();
		}

		public void RemoveIdenticalTo(T item) => RemoveIdenticalTo(new T[] { item });

		public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)Items).GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<T>)Items).GetEnumerator();

		public override bool Equals(object obj) => Equals(obj as GArray<T>);
		public bool Equals(GArray<T> other) => other != null && EqualityComparer<T[]>.Default.Equals(Items, other.Items);
		public override int GetHashCode() => -604923257 + EqualityComparer<T[]>.Default.GetHashCode(Items);

		public static bool operator ==(GArray<T> array1, GArray<T> array2) => EqualityComparer<GArray<T>>.Default.Equals(array1, array2);
		public static bool operator !=(GArray<T> array1, GArray<T> array2) => !(array1 == array2);
	}
}