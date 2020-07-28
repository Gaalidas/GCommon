using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GCommon.Math;

namespace GCommon.Collections
{
	public class GList
	{
		public static int MinIndex => 0;
		public static GList<T> Empty<T>() => new GList<T>();
	}

	/// <summary>A wrapper for <see cref="List{T}"/> that enables some advanced features.</summary>
	public class GList<T> : GList, IEquatable<GList<T>>, IEnumerable<T>
	{
		#region Enumeration
		IEnumerator<T> IEnumerable<T>.GetEnumerator() => ((IEnumerable<T>)Vals).GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<T>)Vals).GetEnumerator();
		#endregion

		#region Collection
		public List<T> Vals { get; set; } = new List<T>();
		#endregion

		#region This Ops
		public T this[int index]
		{
			get => LastIndex >= index ? Vals[index] : (default);
			set
			{
				if (LastIndex >= index)
					Vals[index] = value;
				else
					Vals.Add(value);
			}
		}
		#endregion

		#region Validations
		public bool IsValid => true;

		public bool TrueForAll
		{
			get
			{
				foreach (T tval1 in Vals)
				{
					switch (tval1)
					{
						case bool outBool:
							if (!outBool)
								return false;
							break;
						default:
							if (tval1 == default)
								return false;
							break;
					}
				}

				return true;
			}
		}

		public int Capacity
		{
			get => InternalCapacity > 0 ? InternalCapacity : Vals.Capacity;
			set
			{
				if (Capacity > value)
				{
					int capacityDiff = Capacity - value;
					Vals.RemoveRange(value - 1, capacityDiff);
				}

				Vals.Capacity = value;
				InternalCapacity = value;
			}
		}

		int InternalCapacity { get; set; } = 0;
		#endregion

		#region Adds
		public void Add(T item, bool checkDistinct = false)
		{
			if (checkDistinct)
				Remove(item);

			Vals.Add(item);
		}

		public void AddRange(T[] input, bool checkDistinct = false)
		{
			for (int i = 0; i < input.Length; i++)
				Add(input[i], checkDistinct);
		}

		public void AddRange(List<T> input, bool checkDistinct = false)
		{
			foreach (T item in input)
				Add(item, checkDistinct);
		}

		public void AddRange(GList<T> input, bool checkDistinct = false)
		{
			foreach (T item in input.Vals)
				Add(item, checkDistinct);
		}

		public void InsertAt(T item, int index)
		{
			index = index.Clamp(0, LastIndex);
			GList<T> remainderItems = Empty<T>();

			for (int i = index; i < Count; i++)
				remainderItems.Add(Vals[i]);

			RemoveBetween(index, LastIndex);
			Add(item);
			AddRange(remainderItems);
		}

		public void InsertStart(T item) => InsertAt(item, 0);
		#endregion

		#region Removes
		public void Remove(T item)
		{
			if (Contains(item))
				Vals.RemoveAt(IndexOf(item));
		}

		public void RemoveAt(int index)
		{
			if (index <= LastIndex)
				Vals.RemoveAt(index);
		}

		public void RemoveBetween(int startIndex, int endIndex)
		{
			startIndex = startIndex.ClampMin(0);
			endIndex = endIndex.ClampMax(LastIndex);
			RemoveRange(startIndex, endIndex - startIndex);
		}

		public void RemoveRange(int index, int count) => Vals.RemoveRange(index, count);
		public void RemoveFirst() => RemoveAt(0);
		public void RemoveLast() => RemoveAt(LastIndex);
		#endregion

		#region Extractions
		/// <summary>Grabs all entities from the starting index to the index at the count, or the last index found, whichever comes first.</summary>
		/// <returns>A new <see cref="GList{T}"/> with the entities requested.</returns>
		public GList<T> ExtractRange(int index, int count)
		{
			GList<T> output = Empty<T>();
			count = count.ClampMax(LastIndex);

			for (int i = index; i <= count; i++)
			{
				if (i > LastIndex)
					break;
				else
					output.Add(Vals[i]);
			}

			return output;
		}

		public T GetLast() => Vals.Count > 0 ? Vals[LastIndex] : (default);
		public T GetFirst() => Vals.Count > 0 ? Vals[0] : (default);
		#endregion

		#region Constructors
		public GList() { }
		public GList(List<T> vals) => Vals = vals;
		public GList(List<T> vals, int capacity) : this(vals) => Capacity = capacity;
		public GList(int capacity) => Capacity = capacity;
		public GList(T item) => Vals.Add(item);
		public GList(T[] items) => Vals = items.ToList();
		#endregion

		#region Indexing
		public int IndexOf(T item) => Contains(item) ? Vals.IndexOf(item) : -1;

		public int Count => Vals.Count;
		public int LastIndex => Count - 1;
		#endregion

		#region Contains
		public bool Contains(T item) => Vals.Contains(item);

		public bool Contains(T item, out int index)
		{
			index = -1;

			if (Contains(item))
				index = Vals.IndexOf(item);

			return index != -1;
		}
		#endregion

		#region Overrides
		public override string ToString() => Vals.ToString();
		public override bool Equals(object obj) => Equals(obj as GList<T>);
		public override int GetHashCode() => -2087177403 + EqualityComparer<List<T>>.Default.GetHashCode(Vals);
		#endregion

		#region Equality Ops
		public bool Equals(GList<T> other) => other != null && EqualityComparer<List<T>>.Default.Equals(Vals, other.Vals);
		public static bool operator ==(GList<T> list1, GList<T> list2) => EqualityComparer<GList<T>>.Default.Equals(list1, list2);
		public static bool operator !=(GList<T> list1, GList<T> list2) => !(list1 == list2);
		#endregion

		#region List Conversions
		public void CopyTo(out T[] array) => array = Count != 0 ? Vals.ToArray() : Array.Empty<T>();
		public void CopyT0(out List<T> list) => list = Vals ?? new List<T>();

		public List<T> ToList() => Vals;

		public T[] ToArray()
		{
			T[] output = new T[Count];

			for (int i = 0; i < Count; i++)
				output[i] = Vals[i];

			return output;
		}
		#endregion

		#region ForEachAction
		public void ForEach(Action<T> action)
		{
			if (action == null)
				return;

			Vals.ForEach(action);
		}
		#endregion

		#region Containser Ops
		public void Clear() => Vals.Clear();
		#endregion
	}
}