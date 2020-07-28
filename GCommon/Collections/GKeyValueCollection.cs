using System;
using System.Collections;
using System.Collections.Generic;

namespace GCommon.Collections
{
	public class GKeyValueCollection
	{
		public bool Contains<T>(T item, GList<T> inputList) => inputList != null && inputList.Count > 0 && inputList.IsValid && item != null ? inputList.Contains(item) : (default);
		public int IndexOf<T>(T item, GList<T> inputList) => inputList != null && inputList.Count > 0 && inputList.IsValid && item != null && inputList.Contains(item) ? inputList.IndexOf(item) : -1;
		public bool IsValid<T1, T2>(GList<T1> input1, GList<T2> input2) => input1.IsValid && input2.IsValid && input1.Count == input2.Count;
		public static GKeyValueCollection<TKey, TVal> Empty<TKey, TVal>() => new GKeyValueCollection<TKey, TVal>();
	}

	public class GKeyValueCollection<TKey, TVal> : GKeyValueCollection, IEnumerable<GKeyValueSet<TKey, TVal>>, IEquatable<GKeyValueCollection<TKey, TVal>>
	{
		public GList<TKey> Keys { get; set; } = new GList<TKey>();
		public GList<TVal> Vals { get; set; } = new GList<TVal>();

		public GList<GKeyValueSet<TKey, TVal>> KeySets
		{
			get
			{
				GList<GKeyValueSet<TKey, TVal>> keySets = new GList<GKeyValueSet<TKey, TVal>>();

				foreach (TKey key in Keys)
					keySets.Add(new GKeyValueSet<TKey, TVal>(key, Vals[IndexOf(key)]));

				return keySets;
			}
			set
			{
				Clear();

				foreach (GKeyValueSet<TKey, TVal> set in value)
				{
					Keys.Add(set.Key);
					Vals.Add(set.Val1);
				}
			}
		}

		public int Count => Keys.Count;
		public int LastIndex => Keys.Count - 1;
		public bool NotEmpty => Keys.Count > 0;

		public bool Contains(TKey key) => Contains(key, Keys);
		public bool Contains(TVal val) => Contains(val, Vals);

		public void Clear()
		{
			Keys.Clear();
			Vals.Clear();
		}

		public bool TrueForAll => Keys.TrueForAll && Vals.TrueForAll;

		public int IndexOf(TKey key) => IndexOf(key, Keys);
		public int IndexOf(TVal val) => IndexOf(val, Vals);
		public bool IsValid => IsValid(Keys, Vals);

		public TVal this[TKey key]
		{
			get => Contains(key) ? Vals[IndexOf(key)] : (default);
			set
			{
				if (Contains(key))
					Vals[IndexOf(key)] = value;
				else
				{
					Keys.Add(key);
					Vals.Add(value);
				}
			}
		}

		public IEnumerator<GKeyValueSet<TKey, TVal>> GetEnumerator() => ((IEnumerable<GKeyValueSet<TKey, TVal>>)KeySets).GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<GKeyValueSet<TKey, TVal>>)KeySets).GetEnumerator();

		public override bool Equals(object obj) => Equals(obj as GKeyValueCollection<TKey, TVal>);
		public bool Equals(GKeyValueCollection<TKey, TVal> other) => other != null && EqualityComparer<GList<TKey>>.Default.Equals(Keys, other.Keys) && EqualityComparer<GList<TVal>>.Default.Equals(Vals, other.Vals);

		public override int GetHashCode()
		{
			int hashCode = 837881266;
			hashCode = (hashCode * -1521134295) + EqualityComparer<GList<TKey>>.Default.GetHashCode(Keys);
			hashCode = (hashCode * -1521134295) + EqualityComparer<GList<TVal>>.Default.GetHashCode(Vals);
			return hashCode;
		}

		public static bool operator ==(GKeyValueCollection<TKey, TVal> collection1, GKeyValueCollection<TKey, TVal> collection2) => EqualityComparer<GKeyValueCollection<TKey, TVal>>.Default.Equals(collection1, collection2);
		public static bool operator !=(GKeyValueCollection<TKey, TVal> collection1, GKeyValueCollection<TKey, TVal> collection2) => !(collection1 == collection2);
	}
}