using System;
using System.Collections;
using System.Collections.Generic;
using GCommon.Math;

namespace GCommon.Collections
{
	/// <summary>A parent class for the common <see cref="GDict{TKey, TVal1}"/> class system.</summary>
	public class GDict
	{
		public virtual int NumCollections => 1;

		public void ForEach<T>(Action<T> action, GList<T> inputList)
		{
			if (inputList != null && inputList.Count > 0 && inputList.IsValid && action != null)
				inputList.ForEach(action);
		}

		public bool Contains<T>(T item, GList<T> inputList) => inputList != null && inputList.Count > 0 && inputList.IsValid && item != null ? inputList.Contains(item) : false;
		public int IndexOf<T>(T item, GList<T> inputList) => inputList != null && inputList.Count > 0 && inputList.IsValid && item != null && inputList.Contains(item) ? inputList.IndexOf(item) : -1;
		public bool TryGetVal<TKey, TVal>(TKey key, GList<TKey> inputKeys, GList<TVal> inputList, out TVal output) => (output = inputKeys.Contains(key) ? inputList[IndexOf(key, inputKeys)] : (default)) != default;
		public bool IsEmpty<TKey>(GList<TKey> inputList) => inputList.Count == 0;

		public static GDict<TK, T1> Empty<TK, T1>() => new GDict<TK, T1>();
		public static GDict<TK, T1, T2> Empty<TK, T1, T2>() => new GDict<TK, T1, T2>();
		public static GDict<TK, T1, T2, T3> Empty<TK, T1, T2, T3>() => new GDict<TK, T1, T2, T3>();
		public static GDict<TK, T1, T2, T3, T4> Empty<TK, T1, T2, T3, T4>() => new GDict<TK, T1, T2, T3, T4>();
	}

	/// <summary>A dictionary with keys and values.</summary>
	public class GDict<TKey, TVal1> : GDict, IEquatable<GDict<TKey, TVal1>>, IEnumerable<GKeyValueSet<TKey, TVal1>>
	{
		#region Collections
		public GList<TKey> Keys { get; set; } = new GList<TKey>();
		public GList<TVal1> Vals1 { get; set; } = new GList<TVal1>();

		public GList<GKeyValueSet<TKey, TVal1>> KeyValueSets
		{
			get
			{
				GList<GKeyValueSet<TKey, TVal1>> keyValSets = new GList<GKeyValueSet<TKey, TVal1>>();

				foreach (TKey key in Keys)
					keyValSets.Add(new GKeyValueSet<TKey, TVal1>(key, Vals1[IndexOf(key)]));

				return keyValSets;
			}
			set
			{
				Clear();

				foreach (GKeyValueSet<TKey, TVal1> keyValueSet in value)
				{
					Keys.Add(keyValueSet.Key);
					Vals1.Add(keyValueSet.Val1);
				}
			}
		}
		#endregion

		#region Validations
		/// <summary>Are all our lists equal in count?</summary>
		public bool IsValid => Keys.Count.AreCountsEqual(Vals1.Count);

		/// <summary>Intended to return whether or not all values are true OR all values have non-default data.</summary>
		public bool TrueForAll
		{
			get
			{
				foreach (TVal1 v1 in Vals1.Vals)
				{
					switch (v1)
					{
						case bool outBool:
							if (!outBool)
								return false;
							break;
						default:
							if (v1 == default)
								return false;
							break;
					}
				}

				return true;
			}
		}

		public bool ListsEqual => Keys.Count == Vals1.Count;
		public bool IsEmpty => IsEmpty(Keys);
		#endregion

		#region ForEachAction
		public void ForEach(Action<TKey> action) => ForEach(action, Keys);
		public void ForEach(Action<TVal1> action) => ForEach(action, Vals1);
		#endregion

		#region Constructors
		public GDict() { }

		public GDict(GList<TKey> keys, GList<TVal1> vals1)
		{
			Keys = keys;
			Vals1 = vals1;
		}

		public GDict(TKey key, TVal1 val1)
		{
			Clear();
			Keys.Add(key);
			Vals1.Add(val1);
		}
		#endregion

		#region Simple Collection Data
		/// <summary>Returns a count of the items contained in the collection. If -1, the collection is not balanced.</summary>
		public int Count => Keys.Count;

		/// <summary>Returns the last valid index in the collection.</summary>
		public int LastIndex => Count - 1;

		/// <summary>Returns true if the last index is valid.</summary>
		public bool NotEmpty => Count > 0;

		public override int NumCollections => 2;
		#endregion

		#region This Ops
		/// <summary>Gets or sets the <see cref="TVal"/> in Vals associated with the <see cref="TKey"/> key entered.</summary>
		public TVal1 this[TKey key]
		{
			get => Contains(key) ? Vals1[IndexOf(key)] : (default);
			set
			{
				if (value != null)
				{
					if (Contains(key))
						Vals1[IndexOf(key)] = value;
					else
						Add(key, value);
				}
			}
		}

		/// <summary>Gets or sets the <see cref="TVal"/> in Vals identified by the index, if in range.</summary>
		public TVal1 this[int index]
		{
			get => LastIndex >= index ? Vals1[index] : (default);
			set
			{
				if (LastIndex >= index && value != null)
					Vals1[index] = value;
			}
		}
		#endregion

		#region Adds
		/// <summary>Adds a new entry into the appropriate lists given the input.</summary>
		/// <param name="checkDistincts">Do we want to remove old entries with identical keys?</param>
		public void Add(TKey key, TVal1 val, bool checkDistincts = false)
		{
			if (checkDistincts)
				RemoveIdenticalTo(key);

			if (!Keys.Contains(key) && IsValid)
			{
				Keys.Add(key);
				Vals1.Add(val);
			}
		}

		/// <summary>Adds the input <see cref="GDict{TKey, TVal1}"/> to the current collection.</summary>
		/// <param name="checkDistinct">Do we want to remove old entries with identical keys?</param>
		public void AddRange(GDict<TKey, TVal1> sourceDict, bool checkDistinct = false)
		{
			if (sourceDict.IsValid)
			{
				if (checkDistinct)
					RemoveIdenticalTo(sourceDict);

				Keys.AddRange(sourceDict.Keys);
				Vals1.AddRange(sourceDict.Vals1);
			}
		}

		/// <summary>Adds the values from the input <see cref="GList{T}"/> objects to the current collection where applicable.</summary>
		/// <param name="checkDistinct">Do we want to remove old entries with identical keys?</param>
		public void AddRange(GList<TKey> keysIn, GList<TVal1> valsIn, bool checkDistinct = false)
		{
			if (CollectionUtils.ListsEqual(keysIn, valsIn))
			{
				if (checkDistinct)
					RemoveIdenticalTo(keysIn);

				if (keysIn.Count == valsIn.Count)
				{
					Keys.AddRange(keysIn);
					Vals1.AddRange(valsIn);
				}
			}
		}
		#endregion

		#region Removes
		/// <summary>Removes identical keys and their values from the collection.</summary>
		/// <param name="keyTarget">A <see cref="TKey"/> target to search for.</param>
		public void Remove(TKey keyTarget)
		{
			if (Keys.Contains(keyTarget))
			{
				int keyIndex = Keys.IndexOf(keyTarget);
				Vals1.RemoveAt(keyIndex);
				Keys.RemoveAt(keyIndex);
			}
		}

		/// <summary>Removes identical keys and their values from the collection.</summary>
		/// <param name="keyIndex">An index to the target key.</param>
		public void RemoveAt(int keyIndex)
		{
			if (Keys.Count - 1 >= keyIndex && Vals1.Count - 1 >= keyIndex)
			{
				Vals1.RemoveAt(keyIndex);
				Keys.RemoveAt(keyIndex);
			}
		}

		/// <summary>Removes all idenctical entries based on the input source key.</summary>
		/// <param name="keySource">A <see cref="TKey"/> to search for.</param>
		public void RemoveIdenticalTo(TKey keySrc)
		{
			foreach (TKey key in Keys.Vals)
			{
				if (Equals(key, keySrc))
					RemoveAt(Keys.IndexOf(key));
			}
		}

		/// <summary>Removes all identical entries based on the input source list.</summary>
		/// <param name="keyList">A <see cref="GList{TKey}"/> of keys to search for.</param>
		public void RemoveIdenticalTo(GList<TKey> keysIn)
		{
			foreach (TKey keySource in keysIn)
			{
				if (Keys.Contains(keySource))
					Remove(keySource);
			}
		}

		/// <summary>Removes all identical entries based on the input source list.</summary>
		/// <param name="keyList">A <see cref="GDict{TKey, TVal1}"/> to search with.</param>
		public void RemoveIdenticalTo(GDict<TKey, TVal1> input)
		{
			foreach (TKey key in input.Keys.Vals)
			{
				if (Keys.Contains(key))
					Remove(key);
			}
		}

		/// <summary>Identical to <see cref="GList{T}.RemoveRange(int, int)"/>.</summary>
		public void RemoveRange(int index, int count)
		{
			if (index + (count - 1) <= LastIndex)
			{
				Vals1.RemoveRange(index, count);
				Keys.RemoveRange(index, count);
			}
		}
		#endregion

		#region Container Ops
		public bool Contains(TKey key) => Contains(key, Keys);
		public bool Contains(TVal1 val) => Contains(val, Vals1);

		public bool ContainsKey(TKey key) => Contains(key, Keys);
		public bool ContainsVal(TVal1 val1) => Contains(val1, Vals1);

		public int IndexOf(TKey key) => IndexOf(key, Keys);
		public int IndexOf(TVal1 val) => IndexOf(val, Vals1);

		public bool TryGetVal(TKey key, out TVal1 out1) => TryGetVal(key, Keys, Vals1, out out1);

		public void Clear()
		{
			Keys.Clear();
			Vals1.Clear();
		}
		#endregion

		#region Overrides
		public override string ToString() => base.ToString();
		#endregion

		#region Equality
		public override bool Equals(object obj) => Equals(obj as GDict<TKey, TVal1>);
		public bool Equals(GDict<TKey, TVal1> other) => other != null && EqualityComparer<GList<TKey>>.Default.Equals(Keys, other.Keys) && EqualityComparer<GList<TVal1>>.Default.Equals(Vals1, other.Vals1);

		public override int GetHashCode()
		{
			int hashCode = 165384537;
			hashCode = (hashCode * -1521134295) + EqualityComparer<GList<TKey>>.Default.GetHashCode(Keys);
			hashCode = (hashCode * -1521134295) + EqualityComparer<GList<TVal1>>.Default.GetHashCode(Vals1);
			return hashCode;
		}

		public static bool operator ==(GDict<TKey, TVal1> dict1, GDict<TKey, TVal1> dict2) => EqualityComparer<GDict<TKey, TVal1>>.Default.Equals(dict1, dict2);
		public static bool operator !=(GDict<TKey, TVal1> dict1, GDict<TKey, TVal1> dict2) => !(dict1 == dict2);
		#endregion

		#region Copy Ops
		public void CopyTo(out TVal1[] arrayVals) => arrayVals = Vals1.Count > 0 ? Vals1.Vals.ToArray() : (default);
		public void CopyTo(out List<TVal1> listVals) => listVals = Vals1.Vals ?? (default);
		public void CopyTo(out GList<TVal1> glistVals) => glistVals = new GList<TVal1>(Vals1.Vals);

		public void CopyTo(out TKey[] arrayKeys) => arrayKeys = Keys.Count > 0 ? Keys.Vals.ToArray() : (default);
		public void CopyTo(out List<TKey> listKeys) => listKeys = Keys.Vals ?? (default);
		public void CopyTo(out GList<TKey> glistKeys) => glistKeys = new GList<TKey>(Keys.Vals);
		#endregion

		#region Enumeration
		public IEnumerator<GKeyValueSet<TKey, TVal1>> GetEnumerator() => ((IEnumerable<GKeyValueSet<TKey, TVal1>>)KeyValueSets).GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<GKeyValueSet<TKey, TVal1>>)KeyValueSets).GetEnumerator();
		#endregion
	}

	/// <summary>A dictionary with keys and two value sets.</summary>
	public class GDict<TKey, TVal1, TVal2> : GDict, IEquatable<GDict<TKey, TVal1, TVal2>>, IEnumerable<GKeyValueSet<TKey, TVal1, TVal2>>
	{
		#region Collections
		public GList<TKey> Keys { get; set; } = new GList<TKey>();
		public GList<TVal1> Vals1 { get; set; } = new GList<TVal1>();
		public GList<TVal2> Vals2 { get; set; } = new GList<TVal2>();

		public GList<GKeyValueSet<TKey, TVal1, TVal2>> KeyValueSets
		{
			get
			{
				GList<GKeyValueSet<TKey, TVal1, TVal2>> keyValSets = new GList<GKeyValueSet<TKey, TVal1, TVal2>>();

				foreach (TKey key in Keys)
					keyValSets.Add(new GKeyValueSet<TKey, TVal1, TVal2>(key, Vals1[IndexOf(key)], Vals2[IndexOf(key)]));

				return keyValSets;
			}
			set
			{
				Clear();

				foreach (GKeyValueSet<TKey, TVal1, TVal2> keyValueSet in value)
				{
					Keys.Add(keyValueSet.Key);
					Vals1.Add(keyValueSet.Val1);
					Vals2.Add(keyValueSet.Val2);
				}
			}
		}
		#endregion

		#region Validations
		/// <summary>Are all our lists equal in count?</summary>
		public bool IsValid => Keys.Count.AreCountsEqual(Vals1.Count, Vals2.Count);

		/// <summary>Intended to return whether or not all values are true OR all values have non-default data.</summary>
		public bool TrueForAll
		{
			get
			{
				bool[] bools = new bool[2] { true, true };

				foreach (TVal1 v1 in Vals1.Vals)
				{
					if (!bools[0])
						continue;

					switch (v1)
					{
						case bool outBool:
							bools[0] = outBool;
							break;
						default:
							bools[0] = v1 != default;
							break;
					}
				}

				foreach (TVal2 v2 in Vals2.Vals)
				{
					if (!bools[1])
						continue;

					switch (v2)
					{
						case bool outBool:
							bools[1] = outBool;
							break;
						default:
							bools[1] = v2 != default;
							break;
					}
				}

				foreach (bool boolItem in bools)
				{
					if (!boolItem)
						return false;
				}

				return true;
			}
		}

		public bool ListsEqual => Keys.Count == Vals1.Count && Vals1.Count == Vals2.Count;
		#endregion

		#region ForEachAction
		public void ForEach(Action<TKey> action) => ForEach(action, Keys);
		public void ForEach(Action<TVal1> action) => ForEach(action, Vals1);
		public void ForEach(Action<TVal2> action) => ForEach(action, Vals2);
		#endregion

		#region Constructors
		public GDict() { }

		public GDict(GList<TKey> keys, GList<TVal1> vals1)
		{
			Keys = keys;
			Vals1 = vals1;
		}

		public GDict(GList<TKey> keys, GList<TVal1> vals1, GList<TVal2> vals2) : this(keys, vals1) => Vals2 = vals2;

		public GDict(TKey key, TVal1 val1, TVal2 val2)
		{
			Clear();
			Keys.Add(key);
			Vals1.Add(val1);
			Vals2.Add(val2);
		}
		#endregion

		#region Simple Collection Data
		/// <summary>Returns a count of the items contained in the collection. If -1, the collection is not balanced.</summary>
		public int Count => Keys.Count;

		/// <summary>Returns the last valid index in the collection.</summary>
		public int LastIndex => Count - 1;

		/// <summary>Returns true if the last index is valid.</summary>
		public bool NotEmpty => Count > 0;

		public override int NumCollections => 3;
		#endregion

		#region This Ops
		public GKeyValueSet<TKey, TVal1, TVal2> this[TKey key]
		{
			get
			{
				if (Contains(key))
				{
					int keyIndex = IndexOf(key);
					return new GKeyValueSet<TKey, TVal1, TVal2>(Keys[keyIndex], Vals1[keyIndex], Vals2[keyIndex]);
				}
				else
					return GKeyValueSet.Empty<TKey, TVal1, TVal2>();
			}
			set
			{
				if (value != null && value.IsValid)
				{
					if (Contains(key))
					{
						Vals1[IndexOf(key)] = value.Val1;
						Vals2[IndexOf(key)] = value.Val2;
					}
					else
					{
						if (Equals(key, value.Key))
							Add(value.Key, value.Val1, value.Val2);
					}
				}
			}
		}

		public GKeyValueSet<TKey, TVal1, TVal2> this[int index]
		{
			get => LastIndex >= index ? new GKeyValueSet<TKey, TVal1, TVal2>(Keys[index], Vals1[index], Vals2[index]) : GKeyValueSet.Empty<TKey, TVal1, TVal2>();
			set
			{
				if (value != null && value.IsValid && LastIndex >= index)
				{
					Vals1[index] = value.Val1;
					Vals2[index] = value.Val2;
				}
			}
		}
		#endregion

		#region Adds
		/// <summary>Adds a new entry into the appropriate lists given the input.</summary>
		/// <param name="checkDistincts">Do we want to remove old entries with identical keys?</param>
		public void Add(TKey key, TVal1 val1, TVal2 val2, bool checkDistincts = false)
		{
			if (checkDistincts)
				RemoveIdenticalTo(key);

			if (!Keys.Contains(key) && IsValid)
			{
				Keys.Add(key);
				Vals1.Add(val1);
				Vals2.Add(val2);
			}
		}

		/// <summary>Adds the input <see cref="GDict{TKey, TVal1}"/> to the current collection.</summary>
		/// <param name="checkDistinct">Do we want to remove old entries with identical keys?</param>
		public void AddRange(GDict<TKey, TVal1, TVal2> sourceDict, bool checkDistinct = false)
		{
			if (sourceDict.IsValid)
			{
				if (checkDistinct)
					RemoveIdenticalTo(sourceDict);

				Keys.AddRange(sourceDict.Keys);
				Vals1.AddRange(sourceDict.Vals1);
				Vals2.AddRange(sourceDict.Vals2);
			}
		}

		/// <summary>Adds the values from the input <see cref="GList{T}"/> objects to the current collection where applicable.</summary>
		/// <param name="checkDistinct">Do we want to remove old entries with identical keys?</param>
		public void AddRange(GList<TKey> keysIn, GList<TVal1> valsIn1, GList<TVal2> valsIn2, bool checkDistinct = false)
		{
			if (CollectionUtils.ListsEqual(keysIn, valsIn1, valsIn2))
			{
				if (checkDistinct)
					RemoveIdenticalTo(keysIn);

				if (keysIn.Count == valsIn1.Count)
				{
					Keys.AddRange(keysIn);
					Vals1.AddRange(valsIn1);
					Vals2.AddRange(valsIn2);
				}
			}
		}
		#endregion

		#region Removes
		/// <summary>Removes identical keys and their values from the collection.</summary>
		/// <param name="keyTarget">A <see cref="TKey"/> target to search for.</param>
		public void Remove(TKey keyTarget)
		{
			if (Keys.Contains(keyTarget))
			{
				int keyIndex = Keys.IndexOf(keyTarget);
				Vals2.RemoveAt(keyIndex);
				Vals1.RemoveAt(keyIndex);
				Keys.RemoveAt(keyIndex);
			}
		}

		/// <summary>Removes identical keys and their values from the collection.</summary>
		/// <param name="keyIndex">An index to the target key.</param>
		public void RemoveAt(int keyIndex)
		{
			if (Keys.Count - 1 >= keyIndex && Vals1.Count - 1 >= keyIndex && Vals2.Count - 1 >= keyIndex)
			{
				Vals2.RemoveAt(keyIndex);
				Vals1.RemoveAt(keyIndex);
				Keys.RemoveAt(keyIndex);
			}
		}

		/// <summary>Removes all idenctical entries based on the input source key.</summary>
		/// <param name="keySource">A <see cref="TKey"/> to search for.</param>
		public void RemoveIdenticalTo(TKey keySrc)
		{
			foreach (TKey key in Keys.Vals)
			{
				if (Equals(key, keySrc))
					RemoveAt(Keys.IndexOf(key));
			}
		}

		/// <summary>Removes all identical entries based on the input source list.</summary>
		/// <param name="keyList">A <see cref="GList{TKey}"/> of keys to search for.</param>
		public void RemoveIdenticalTo(GList<TKey> keysIn)
		{
			foreach (TKey keySource in keysIn)
			{
				if (Keys.Contains(keySource))
					Remove(keySource);
			}
		}

		public void RemoveIdenticalTo(GDict<TKey, TVal1, TVal2> input)
		{
			foreach (TKey key in input.Keys.Vals)
			{
				if (Keys.Contains(key))
					Remove(key);
			}
		}

		public void RemoveRange(int index, int count)
		{
			if (index + (count - 1) <= LastIndex)
			{
				Vals2.RemoveRange(index, count);
				Vals1.RemoveRange(index, count);
				Keys.RemoveRange(index, count);
			}
		}
		#endregion

		#region Container Ops
		public bool Contains(TKey key) => Contains(key, Keys);
		public bool Contains(TVal1 val) => Contains(val, Vals1);
		public bool Contains(TVal2 val) => Contains(val, Vals2);

		public bool ContainsKey(TKey key) => Contains(key, Keys);
		public bool ContainsVal(TVal1 val1) => Contains(val1, Vals1);
		public bool ContainsVal(TVal2 val2) => Contains(val2, Vals2);

		public int IndexOf(TKey key) => IndexOf(key, Keys);
		public int IndexOf(TVal1 val) => IndexOf(val, Vals1);
		public int IndexOf(TVal2 val) => IndexOf(val, Vals2);

		public bool TryGetVal(TKey key, out TVal1 out1) => TryGetVal(key, Keys, Vals1, out out1);
		public bool TryGetVal(TKey key, out TVal2 out2) => TryGetVal(key, Keys, Vals2, out out2);

		public void Clear()
		{
			Keys.Clear();
			Vals1.Clear();
			Vals2.Clear();
		}
		#endregion

		#region Overrides
		public override string ToString() => base.ToString();
		#endregion

		#region Equality
		public override bool Equals(object obj) => Equals(obj as GDict<TKey, TVal1, TVal2>);
		public bool Equals(GDict<TKey, TVal1, TVal2> other) => other != null && EqualityComparer<GList<TKey>>.Default.Equals(Keys, other.Keys) && EqualityComparer<GList<TVal1>>.Default.Equals(Vals1, other.Vals1) && EqualityComparer<GList<TVal2>>.Default.Equals(Vals2, other.Vals2);

		public override int GetHashCode()
		{
			int hashCode = 165384537;
			hashCode = (hashCode * -1521134295) + EqualityComparer<GList<TKey>>.Default.GetHashCode(Keys);
			hashCode = (hashCode * -1521134295) + EqualityComparer<GList<TVal1>>.Default.GetHashCode(Vals1);
			hashCode = (hashCode * -1521134295) + EqualityComparer<GList<TVal2>>.Default.GetHashCode(Vals2);
			return hashCode;
		}

		public static bool operator ==(GDict<TKey, TVal1, TVal2> dict1, GDict<TKey, TVal1, TVal2> dict2) => EqualityComparer<GDict<TKey, TVal1, TVal2>>.Default.Equals(dict1, dict2);
		public static bool operator !=(GDict<TKey, TVal1, TVal2> dict1, GDict<TKey, TVal1, TVal2> dict2) => !(dict1 == dict2);
		#endregion

		#region Copy Ops
		public void CopyTo(out TVal1[] arrayVals) => arrayVals = Vals1.Count > 0 ? Vals1.Vals.ToArray() : (default);
		public void CopyTo(out List<TVal1> listVals) => listVals = Vals1.Vals ?? (default);
		public void CopyTo(out GList<TVal1> glistVals) => glistVals = new GList<TVal1>(Vals1.Vals);

		public void CopyTo(out TVal2[] arrayVals) => arrayVals = Vals2.Count > 0 ? Vals2.Vals.ToArray() : (default);
		public void CopyTo(out List<TVal2> listVals) => listVals = Vals2.Vals ?? (default);
		public void CopyTo(out GList<TVal2> glistVals) => glistVals = new GList<TVal2>(Vals2.Vals);

		public void CopyTo(out TKey[] arrayKeys) => arrayKeys = Keys.Count > 0 ? Keys.Vals.ToArray() : (default);
		public void CopyTo(out List<TKey> listKeys) => listKeys = Keys.Vals ?? (default);
		public void CopyTo(out GList<TKey> glistKeys) => glistKeys = new GList<TKey>(Keys.Vals);
		#endregion

		#region Enumeration
		public IEnumerator<GKeyValueSet<TKey, TVal1, TVal2>> GetEnumerator() => ((IEnumerable<GKeyValueSet<TKey, TVal1, TVal2>>)KeyValueSets).GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<GKeyValueSet<TKey, TVal1, TVal2>>)KeyValueSets).GetEnumerator();
		#endregion
	}

	/// <summary>A dictionary with keys and three value sets.</summary>
	public class GDict<TKey, TVal1, TVal2, TVal3> : GDict, IEquatable<GDict<TKey, TVal1, TVal2, TVal3>>, IEnumerable<GKeyValueSet<TKey, TVal1, TVal2, TVal3>>
	{
		#region Collections
		public GList<TKey> Keys { get; set; } = new GList<TKey>();
		public GList<TVal1> Vals1 { get; set; } = new GList<TVal1>();
		public GList<TVal2> Vals2 { get; set; } = new GList<TVal2>();
		public GList<TVal3> Vals3 { get; set; } = new GList<TVal3>();

		public GList<GKeyValueSet<TKey, TVal1, TVal2, TVal3>> KeyValueSets
		{
			get
			{
				GList<GKeyValueSet<TKey, TVal1, TVal2, TVal3>> keyValSets = new GList<GKeyValueSet<TKey, TVal1, TVal2, TVal3>>();

				foreach (TKey key in Keys)
					keyValSets.Add(new GKeyValueSet<TKey, TVal1, TVal2, TVal3>(key, Vals1[IndexOf(key)], Vals2[IndexOf(key)], Vals3[IndexOf(key)]));

				return keyValSets;
			}
			set
			{
				Clear();

				foreach (GKeyValueSet<TKey, TVal1, TVal2, TVal3> keyValueSet in value)
				{
					Keys.Add(keyValueSet.Key);
					Vals1.Add(keyValueSet.Val1);
					Vals2.Add(keyValueSet.Val2);
					Vals3.Add(keyValueSet.Val3);
				}
			}
		}
		#endregion

		#region Validations
		/// <summary>Are all our lists equal in count?</summary>
		public bool IsValid => Keys.Count.AreCountsEqual(Vals1.Count, Vals2.Count, Vals3.Count);

		/// <summary>Intended to return whether or not all values are true OR all values have non-default data.</summary>
		public bool TrueForAll
		{
			get
			{
				bool[] bools = new bool[3] { true, true, true };

				foreach (TVal1 v1 in Vals1.Vals)
				{
					if (!bools[0])
						continue;

					switch (v1)
					{
						case bool outBool:
							bools[0] = outBool;
							break;
						default:
							bools[0] = v1 != default;
							break;
					}
				}

				foreach (TVal2 v2 in Vals2.Vals)
				{
					if (!bools[1])
						continue;

					switch (v2)
					{
						case bool outBool:
							bools[1] = outBool;
							break;
						default:
							bools[1] = v2 != default;
							break;
					}
				}

				foreach (TVal3 v3 in Vals3.Vals)
				{
					if (!bools[2])
						continue;

					switch (v3)
					{
						case bool outBool:
							bools[2] = outBool;
							break;
						default:
							bools[2] = v3 != default;
							break;
					}
				}

				foreach (bool boolItem in bools)
				{
					if (!boolItem)
						return false;
				}

				return true;
			}
		}

		public bool ListsEqual => Keys.Count == Vals1.Count && Vals1.Count == Vals2.Count && Vals2.Count == Vals3.Count;
		#endregion

		#region ForEachAction
		public void ForEach(Action<TKey> action) => ForEach(action, Keys);
		public void ForEach(Action<TVal1> action) => ForEach(action, Vals1);
		public void ForEach(Action<TVal2> action) => ForEach(action, Vals2);
		public void ForEach(Action<TVal3> action) => ForEach(action, Vals3);
		#endregion

		#region Simple Collection Data
		/// <summary>Returns a count of the items contained in the collection. If -1, the collection is not balanced.</summary>
		public int Count => Keys.Count;

		/// <summary>Returns the last valid index in the collection.</summary>
		public int LastIndex => Count - 1;

		/// <summary>Returns true if the last index is valid.</summary>
		public bool NotEmpty => Count > 0;

		public override int NumCollections => 4;
		#endregion

		#region This Ops
		public GValueSet<TVal1, TVal2, TVal3> this[TKey key]
		{
			get => Contains(key) ? new GValueSet<TVal1, TVal2, TVal3>(Vals1[IndexOf(key)], Vals2[IndexOf(key)], Vals3[IndexOf(key)]) : (default);
			set
			{
				if (value != null && value.IsValid)
				{
					if (Contains(key))
					{
						Vals1[IndexOf(key)] = value.Val1;
						Vals2[IndexOf(key)] = value.Val2;
						Vals3[IndexOf(key)] = value.Val3;
					}
					else
						Add(key, value.Val1, value.Val2, value.Val3);
				}
			}
		}

		public GValueSet<TVal1, TVal2, TVal3> this[int index]
		{
			get => LastIndex >= index ? new GValueSet<TVal1, TVal2, TVal3>(Vals1[index], Vals2[index], Vals3[index]) : (default);
			set
			{
				if (value != null && value.IsValid && LastIndex >= index)
				{
					Vals1[index] = value.Val1;
					Vals2[index] = value.Val2;
					Vals3[index] = value.Val3;
				}
			}
		}
		#endregion

		#region Constructors
		public GDict() { }

		public GDict(GList<TKey> keys, GList<TVal1> vals1)
		{
			Keys = keys;
			Vals1 = vals1;
		}

		public GDict(GList<TKey> keys, GList<TVal1> vals1, GList<TVal2> vals2) : this(keys, vals1) => Vals2 = vals2;
		public GDict(GList<TKey> keys, GList<TVal1> vals1, GList<TVal2> vals2, GList<TVal3> vals3) : this(keys, vals1, vals2) => Vals3 = vals3;

		public GDict(TKey key, TVal1 val1, TVal2 val2, TVal3 val3)
		{
			Clear();
			Keys.Add(key);
			Vals1.Add(val1);
			Vals2.Add(val2);
			Vals3.Add(val3);
		}
		#endregion

		#region Adds
		/// <summary>Adds a new entry into the appropriate lists given the input.</summary>
		/// <param name="checkDistincts">Do we want to remove old entries with identical keys?</param>
		public void Add(TKey key, TVal1 val1, TVal2 val2, TVal3 val3, bool checkDistincts = false)
		{
			if (checkDistincts)
				RemoveIdenticalTo(key);

			if (!Keys.Contains(key) && IsValid)
			{
				Keys.Add(key);
				Vals1.Add(val1);
				Vals2.Add(val2);
				Vals3.Add(val3);
			}
		}

		/// <summary>Adds the input <see cref="GDict{TKey, TVal1}"/> to the current collection.</summary>
		/// <param name="checkDistinct">Do we want to remove old entries with identical keys?</param>
		public void AddRange(GDict<TKey, TVal1, TVal2, TVal3> sourceDict, bool checkDistinct = false)
		{
			if (sourceDict.IsValid)
			{
				if (checkDistinct)
					RemoveIdenticalTo(sourceDict);

				Keys.AddRange(sourceDict.Keys);
				Vals1.AddRange(sourceDict.Vals1);
				Vals2.AddRange(sourceDict.Vals2);
				Vals3.AddRange(sourceDict.Vals3);
			}
		}

		/// <summary>Adds the values from the input <see cref="GList{T}"/> objects to the current collection where applicable.</summary>
		/// <param name="checkDistinct">Do we want to remove old entries with identical keys?</param>
		public void AddRange(GList<TKey> keysIn, GList<TVal1> valsIn1, GList<TVal2> valsIn2, GList<TVal3> valsIn3, bool checkDistinct = false)
		{
			if (CollectionUtils.ListsEqual(keysIn, valsIn1, valsIn2, valsIn3))
			{
				if (checkDistinct)
					RemoveIdenticalTo(keysIn);

				if (keysIn.Count == valsIn1.Count)
				{
					Keys.AddRange(keysIn);
					Vals1.AddRange(valsIn1);
					Vals2.AddRange(valsIn2);
					Vals3.AddRange(valsIn3);
				}
			}
		}
		#endregion

		#region Removes
		/// <summary>Removes identical keys and their values from the collection.</summary>
		/// <param name="keyTarget">A <see cref="TKey"/> target to search for.</param>
		public void Remove(TKey keyTarget)
		{
			if (Keys.Contains(keyTarget))
			{
				int keyIndex = Keys.IndexOf(keyTarget);
				Vals3.RemoveAt(keyIndex);
				Vals2.RemoveAt(keyIndex);
				Vals1.RemoveAt(keyIndex);
				Keys.RemoveAt(keyIndex);
			}
		}

		/// <summary>Removes identical keys and their values from the collection.</summary>
		/// <param name="keyIndex">An index to the target key.</param>
		public void RemoveAt(int keyIndex)
		{
			if (Keys.Count - 1 >= keyIndex && Vals1.Count - 1 >= keyIndex && Vals2.Count - 1 >= keyIndex && Vals3.Count - 1 >= keyIndex)
			{
				Vals3.RemoveAt(keyIndex);
				Vals2.RemoveAt(keyIndex);
				Vals1.RemoveAt(keyIndex);
				Keys.RemoveAt(keyIndex);
			}
		}

		/// <summary>Removes all idenctical entries based on the input source key.</summary>
		/// <param name="keySource">A <see cref="TKey"/> to search for.</param>
		public void RemoveIdenticalTo(TKey keySrc)
		{
			foreach (TKey key in Keys.Vals)
			{
				if (Equals(key, keySrc))
					RemoveAt(Keys.IndexOf(key));
			}
		}

		/// <summary>Removes all identical entries based on the input source list.</summary>
		/// <param name="keyList">A <see cref="GList{TKey}"/> of keys to search for.</param>
		public void RemoveIdenticalTo(GList<TKey> keysIn)
		{
			foreach (TKey keySource in keysIn)
			{
				if (Keys.Contains(keySource))
					Remove(keySource);
			}
		}

		public void RemoveIdenticalTo(GDict<TKey, TVal1, TVal2, TVal3> input)
		{
			foreach (TKey key in input.Keys.Vals)
			{
				if (Keys.Contains(key))
					Remove(key);
			}
		}

		public void RemoveRange(int index, int count)
		{
			if (index + (count - 1) <= LastIndex)
			{
				Vals3.RemoveRange(index, count);
				Vals2.RemoveRange(index, count);
				Vals1.RemoveRange(index, count);
				Keys.RemoveRange(index, count);
			}
		}
		#endregion

		#region Container Ops
		public bool Contains(TKey key) => Contains(key, Keys);
		public bool Contains(TVal1 val) => Contains(val, Vals1);
		public bool Contains(TVal2 val) => Contains(val, Vals2);
		public bool Contains(TVal3 val) => Contains(val, Vals3);

		public bool ContainsKey(TKey key) => Contains(key, Keys);
		public bool ContainsVal(TVal1 val1) => Contains(val1, Vals1);
		public bool ContainsVal(TVal2 val2) => Contains(val2, Vals2);
		public bool ContainsVal(TVal3 val3) => Contains(val3, Vals3);

		public int IndexOf(TKey key) => IndexOf(key, Keys);
		public int IndexOf(TVal1 val) => IndexOf(val, Vals1);
		public int IndexOf(TVal2 val) => IndexOf(val, Vals2);
		public int IndexOf(TVal3 val) => IndexOf(val, Vals3);

		public bool TryGetVal(TKey key, out TVal1 out1) => TryGetVal(key, Keys, Vals1, out out1);
		public bool TryGetVal(TKey key, out TVal2 out2) => TryGetVal(key, Keys, Vals2, out out2);
		public bool TryGetVal(TKey key, out TVal3 out3) => TryGetVal(key, Keys, Vals3, out out3);

		public void Clear()
		{
			Keys.Clear();
			Vals1.Clear();
			Vals2.Clear();
			Vals3.Clear();
		}
		#endregion

		#region Overrides
		public override string ToString() => base.ToString();
		#endregion

		#region Equality
		public override bool Equals(object obj) => Equals(obj as GDict<TKey, TVal1, TVal2, TVal3>);
		public bool Equals(GDict<TKey, TVal1, TVal2, TVal3> other) => other != null && EqualityComparer<GList<TKey>>.Default.Equals(Keys, other.Keys) && EqualityComparer<GList<TVal1>>.Default.Equals(Vals1, other.Vals1) && EqualityComparer<GList<TVal2>>.Default.Equals(Vals2, other.Vals2) && EqualityComparer<GList<TVal3>>.Default.Equals(Vals3, other.Vals3);

		public override int GetHashCode()
		{
			int hashCode = 165384537;
			hashCode = (hashCode * -1521134295) + EqualityComparer<GList<TKey>>.Default.GetHashCode(Keys);
			hashCode = (hashCode * -1521134295) + EqualityComparer<GList<TVal1>>.Default.GetHashCode(Vals1);
			hashCode = (hashCode * -1521134295) + EqualityComparer<GList<TVal2>>.Default.GetHashCode(Vals2);
			hashCode = (hashCode * -1521134295) + EqualityComparer<GList<TVal3>>.Default.GetHashCode(Vals3);
			return hashCode;
		}

		public static bool operator ==(GDict<TKey, TVal1, TVal2, TVal3> dict1, GDict<TKey, TVal1, TVal2, TVal3> dict2) => EqualityComparer<GDict<TKey, TVal1, TVal2, TVal3>>.Default.Equals(dict1, dict2);
		public static bool operator !=(GDict<TKey, TVal1, TVal2, TVal3> dict1, GDict<TKey, TVal1, TVal2, TVal3> dict2) => !(dict1 == dict2);
		#endregion

		#region Copy Ops
		public void CopyTo(out TVal1[] arrayVals) => arrayVals = Vals1.Count > 0 ? Vals1.Vals.ToArray() : (default);
		public void CopyTo(out List<TVal1> listVals) => listVals = Vals1.Vals ?? (default);
		public void CopyTo(out GList<TVal1> glistVals) => glistVals = new GList<TVal1>(Vals1.Vals);

		public void CopyTo(out TVal2[] arrayVals) => arrayVals = Vals2.Count > 0 ? Vals2.Vals.ToArray() : (default);
		public void CopyTo(out List<TVal2> listVals) => listVals = Vals2.Vals ?? (default);
		public void CopyTo(out GList<TVal2> glistVals) => glistVals = new GList<TVal2>(Vals2.Vals);

		public void CopyTo(out TVal3[] arrayVals) => arrayVals = Vals3.Count > 0 ? Vals3.Vals.ToArray() : (default);
		public void CopyTo(out List<TVal3> listVals) => listVals = Vals3.Vals ?? (default);
		public void CopyTo(out GList<TVal3> glistVals) => glistVals = new GList<TVal3>(Vals3.Vals);

		public void CopyTo(out TKey[] arrayKeys) => arrayKeys = Keys.Count > 0 ? Keys.Vals.ToArray() : (default);
		public void CopyTo(out List<TKey> listKeys) => listKeys = Keys.Vals ?? (default);
		public void CopyTo(out GList<TKey> glistKeys) => glistKeys = new GList<TKey>(Keys.Vals);
		#endregion

		#region Enumeration
		public IEnumerator<GKeyValueSet<TKey, TVal1, TVal2, TVal3>> GetEnumerator() => ((IEnumerable<GKeyValueSet<TKey, TVal1, TVal2, TVal3>>)KeyValueSets).GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<GKeyValueSet<TKey, TVal1, TVal2, TVal3>>)KeyValueSets).GetEnumerator();
		#endregion
	}

	/// <summary>A dictionary with keys and four value sets.</summary>
	public class GDict<TKey, TVal1, TVal2, TVal3, TVal4> : GDict, IEquatable<GDict<TKey, TVal1, TVal2, TVal3, TVal4>>, IEnumerable<GKeyValueSet<TKey, TVal1, TVal2, TVal3, TVal4>>
	{
		#region Collections
		public GList<TKey> Keys { get; set; } = new GList<TKey>();
		public GList<TVal1> Vals1 { get; set; } = new GList<TVal1>();
		public GList<TVal2> Vals2 { get; set; } = new GList<TVal2>();
		public GList<TVal3> Vals3 { get; set; } = new GList<TVal3>();
		public GList<TVal4> Vals4 { get; set; } = new GList<TVal4>();

		public GList<GKeyValueSet<TKey, TVal1, TVal2, TVal3, TVal4>> KeyValueSets
		{
			get
			{
				GList<GKeyValueSet<TKey, TVal1, TVal2, TVal3, TVal4>> keyValSets = new GList<GKeyValueSet<TKey, TVal1, TVal2, TVal3, TVal4>>();

				foreach (TKey key in Keys)
					keyValSets.Add(new GKeyValueSet<TKey, TVal1, TVal2, TVal3, TVal4>(key, Vals1[IndexOf(key)], Vals2[IndexOf(key)], Vals3[IndexOf(key)], Vals4[IndexOf(key)]));

				return keyValSets;
			}
			set
			{
				Clear();

				foreach (GKeyValueSet<TKey, TVal1, TVal2, TVal3, TVal4> keyValueSet in value)
				{
					Keys.Add(keyValueSet.Key);
					Vals1.Add(keyValueSet.Val1);
					Vals2.Add(keyValueSet.Val2);
					Vals3.Add(keyValueSet.Val3);
					Vals4.Add(keyValueSet.Val4);
				}
			}
		}
		#endregion

		#region Validations
		/// <summary>Are all our lists equal in count?</summary>
		public bool IsValid => Keys.Count.AreCountsEqual(Vals1.Count, Vals2.Count, Vals3.Count, Vals4.Count);

		/// <summary>Intended to return whether or not all values are true OR all values have non-default data.</summary>
		public bool TrueForAll
		{
			get
			{
				bool[] bools = new bool[4] { true, true, true, true };

				foreach (TVal1 v1 in Vals1.Vals)
				{
					if (!bools[0])
						continue;

					switch (v1)
					{
						case bool outBool:
							bools[0] = outBool;
							break;
						default:
							bools[0] = v1 != default;
							break;
					}
				}

				foreach (TVal2 v2 in Vals2.Vals)
				{
					if (!bools[1])
						continue;

					switch (v2)
					{
						case bool outBool:
							bools[1] = outBool;
							break;
						default:
							bools[1] = v2 != default;
							break;
					}
				}

				foreach (TVal3 v3 in Vals3.Vals)
				{
					if (!bools[2])
						continue;

					switch (v3)
					{
						case bool outBool:
							bools[2] = outBool;
							break;
						default:
							bools[2] = v3 != default;
							break;
					}
				}

				foreach (TVal4 v4 in Vals4.Vals)
				{
					if (!bools[2])
						continue;

					switch (v4)
					{
						case bool outBool:
							bools[3] = outBool;
							break;
						default:
							bools[3] = v4 != default;
							break;
					}
				}

				foreach (bool boolItem in bools)
				{
					if (!boolItem)
						return false;
				}

				return true;
			}
		}

		public bool ListsEqual => Keys.Count == Vals1.Count && Vals1.Count == Vals2.Count && Vals2.Count == Vals3.Count && Vals3.Count == Vals4.Count;
		#endregion

		#region ForEachAction
		public void ForEach(Action<TKey> action) => ForEach(action, Keys);
		public void ForEach(Action<TVal1> action) => ForEach(action, Vals1);
		public void ForEach(Action<TVal2> action) => ForEach(action, Vals2);
		public void ForEach(Action<TVal3> action) => ForEach(action, Vals3);
		public void ForEach(Action<TVal4> action) => ForEach(action, Vals4);
		#endregion

		#region Simple Collection Data
		/// <summary>Returns a count of the items contained in the collection. If -1, the collection is not balanced.</summary>
		public int Count => Keys.Count;

		/// <summary>Returns the last valid index in the collection.</summary>
		public int LastIndex => Count - 1;

		/// <summary>Returns true if the last index is valid.</summary>
		public bool NotEmpty => Count > 0;

		public override int NumCollections => 5;
		#endregion

		#region This Ops
		public GValueSet<TVal1, TVal2, TVal3, TVal4> this[TKey key]
		{
			get => Contains(key) ? new GValueSet<TVal1, TVal2, TVal3, TVal4>(Vals1[IndexOf(key)], Vals2[IndexOf(key)], Vals3[IndexOf(key)], Vals4[IndexOf(key)]) : (default);
			set
			{
				if (value != null && value.IsValid)
				{
					if (Contains(key))
					{
						Vals1[IndexOf(key)] = value.Val1;
						Vals2[IndexOf(key)] = value.Val2;
						Vals3[IndexOf(key)] = value.Val3;
						Vals4[IndexOf(key)] = value.Val4;
					}
					else
						Add(key, value.Val1, value.Val2, value.Val3, value.Val4);
				}
			}
		}

		public GValueSet<TVal1, TVal2, TVal3, TVal4> this[int index]
		{
			get => LastIndex >= index ? new GValueSet<TVal1, TVal2, TVal3, TVal4>(Vals1[index], Vals2[index], Vals3[index], Vals4[index]) : (default);
			set
			{
				if (value != null && value.IsValid && LastIndex >= index)
				{
					Vals1[index] = value.Val1;
					Vals2[index] = value.Val2;
					Vals3[index] = value.Val3;
					Vals4[index] = value.Val4;
				}
			}
		}
		#endregion

		#region Constructors
		public GDict() { }

		public GDict(GList<TKey> keys, GList<TVal1> vals1)
		{
			Keys = keys;
			Vals1 = vals1;
		}

		public GDict(GList<TKey> keys, GList<TVal1> vals1, GList<TVal2> vals2) : this(keys, vals1) => Vals2 = vals2;
		public GDict(GList<TKey> keys, GList<TVal1> vals1, GList<TVal2> vals2, GList<TVal3> vals3) : this(keys, vals1, vals2) => Vals3 = vals3;
		public GDict(GList<TKey> keys, GList<TVal1> vals1, GList<TVal2> vals2, GList<TVal3> vals3, GList<TVal4> vals4) : this(keys, vals1, vals2, vals3) => Vals4 = vals4;

		public GDict(TKey key, TVal1 val1, TVal2 val2, TVal3 val3, TVal4 val4)
		{
			Clear();
			Keys.Add(key);
			Vals1.Add(val1);
			Vals2.Add(val2);
			Vals3.Add(val3);
			Vals4.Add(val4);
		}
		#endregion

		#region Adds
		/// <summary>Adds a new entry into the appropriate lists given the input.</summary>
		/// <param name="checkDistincts">Do we want to remove old entries with identical keys?</param>
		public void Add(TKey key, TVal1 val1, TVal2 val2, TVal3 val3, TVal4 val4, bool checkDistincts = false)
		{
			if (checkDistincts)
				RemoveIdenticalTo(key);

			if (!Keys.Contains(key) && IsValid)
			{
				Keys.Add(key);
				Vals1.Add(val1);
				Vals2.Add(val2);
				Vals3.Add(val3);
				Vals4.Add(val4);
			}
		}

		/// <summary>Adds the input <see cref="GDict{TKey, TVal1}"/> to the current collection.</summary>
		/// <param name="checkDistinct">Do we want to remove old entries with identical keys?</param>
		public void AddRange(GDict<TKey, TVal1, TVal2, TVal3, TVal4> sourceDict, bool checkDistinct = false)
		{
			if (sourceDict.IsValid)
			{
				if (checkDistinct)
					RemoveIdenticalTo(sourceDict);

				Keys.AddRange(sourceDict.Keys);
				Vals1.AddRange(sourceDict.Vals1);
				Vals2.AddRange(sourceDict.Vals2);
				Vals3.AddRange(sourceDict.Vals3);
				Vals4.AddRange(sourceDict.Vals4);
			}
		}

		/// <summary>Adds the values from the input <see cref="GList{T}"/> objects to the current collection where applicable.</summary>
		/// <param name="checkDistinct">Do we want to remove old entries with identical keys?</param>
		public void AddRange(GList<TKey> keysIn, GList<TVal1> valsIn1, GList<TVal2> valsIn2, GList<TVal3> valsIn3, GList<TVal4> valsIn4, bool checkDistinct = false)
		{
			if (CollectionUtils.ListsEqual(keysIn, valsIn1, valsIn2, valsIn3, valsIn4))
			{
				if (checkDistinct)
					RemoveIdenticalTo(keysIn);

				if (keysIn.Count == valsIn1.Count)
				{
					Keys.AddRange(keysIn);
					Vals1.AddRange(valsIn1);
					Vals2.AddRange(valsIn2);
					Vals3.AddRange(valsIn3);
					Vals4.AddRange(valsIn4);
				}
			}
		}
		#endregion

		#region Removes
		/// <summary>Removes identical keys and their values from the collection.</summary>
		/// <param name="keyTarget">A <see cref="TKey"/> target to search for.</param>
		public void Remove(TKey keyTarget)
		{
			if (Keys.Contains(keyTarget))
			{
				int keyIndex = Keys.IndexOf(keyTarget);
				Vals4.RemoveAt(keyIndex);
				Vals3.RemoveAt(keyIndex);
				Vals2.RemoveAt(keyIndex);
				Vals1.RemoveAt(keyIndex);
				Keys.RemoveAt(keyIndex);
			}
		}

		/// <summary>Removes identical keys and their values from the collection.</summary>
		/// <param name="keyIndex">An index to the target key.</param>
		public void RemoveAt(int keyIndex)
		{
			if (Keys.Count - 1 >= keyIndex && Vals1.Count - 1 >= keyIndex && Vals2.Count - 1 >= keyIndex && Vals3.Count - 1 >= keyIndex && Vals4.Count - 1 >= keyIndex)
			{
				Vals4.RemoveAt(keyIndex);
				Vals3.RemoveAt(keyIndex);
				Vals2.RemoveAt(keyIndex);
				Vals1.RemoveAt(keyIndex);
				Keys.RemoveAt(keyIndex);
			}
		}

		/// <summary>Removes all idenctical entries based on the input source key.</summary>
		/// <param name="keySource">A <see cref="TKey"/> to search for.</param>
		public void RemoveIdenticalTo(TKey keySrc)
		{
			foreach (TKey key in Keys.Vals)
			{
				if (Equals(key, keySrc))
					RemoveAt(Keys.IndexOf(key));
			}
		}

		/// <summary>Removes all identical entries based on the input source list.</summary>
		/// <param name="keyList">A <see cref="GList{TKey}"/> of keys to search for.</param>
		public void RemoveIdenticalTo(GList<TKey> keysIn)
		{
			foreach (TKey keySource in keysIn)
			{
				if (Keys.Contains(keySource))
					Remove(keySource);
			}
		}

		public void RemoveIdenticalTo(GDict<TKey, TVal1, TVal2, TVal3, TVal4> input)
		{
			foreach (TKey key in input.Keys.Vals)
			{
				if (Keys.Contains(key))
					Remove(key);
			}
		}

		public void RemoveRange(int index, int count)
		{
			if (index + (count - 1) <= LastIndex)
			{
				Vals4.RemoveRange(index, count);
				Vals3.RemoveRange(index, count);
				Vals2.RemoveRange(index, count);
				Vals1.RemoveRange(index, count);
				Keys.RemoveRange(index, count);
			}
		}
		#endregion

		#region Container Ops
		public bool Contains(TKey key) => Contains(key, Keys);
		public bool Contains(TVal1 val) => Contains(val, Vals1);
		public bool Contains(TVal2 val) => Contains(val, Vals2);
		public bool Contains(TVal3 val) => Contains(val, Vals3);
		public bool Contains(TVal4 val) => Contains(val, Vals4);

		public bool ContainsKey(TKey key) => Contains(key, Keys);
		public bool ContainsVal(TVal1 val1) => Contains(val1, Vals1);
		public bool ContainsVal(TVal2 val2) => Contains(val2, Vals2);
		public bool ContainsVal(TVal3 val3) => Contains(val3, Vals3);
		public bool ContainsVal(TVal4 val4) => Contains(val4, Vals4);

		public int IndexOf(TKey key) => IndexOf(key, Keys);
		public int IndexOf(TVal1 val) => IndexOf(val, Vals1);
		public int IndexOf(TVal2 val) => IndexOf(val, Vals2);
		public int IndexOf(TVal3 val) => IndexOf(val, Vals3);
		public int IndexOf(TVal4 val) => IndexOf(val, Vals4);

		public bool TryGetVal(TKey key, out TVal1 out1) => TryGetVal(key, Keys, Vals1, out out1);
		public bool TryGetVal(TKey key, out TVal2 out2) => TryGetVal(key, Keys, Vals2, out out2);
		public bool TryGetVal(TKey key, out TVal3 out3) => TryGetVal(key, Keys, Vals3, out out3);
		public bool TryGetVal(TKey key, out TVal4 out4) => TryGetVal(key, Keys, Vals4, out out4);

		public void Clear()
		{
			Keys.Clear();
			Vals1.Clear();
			Vals2.Clear();
			Vals3.Clear();
			Vals4.Clear();
		}
		#endregion

		#region Overrides
		public override string ToString() => base.ToString();
		#endregion

		#region Equality
		public override bool Equals(object obj) => Equals(obj as GDict<TKey, TVal1, TVal2, TVal3, TVal4>);
		public bool Equals(GDict<TKey, TVal1, TVal2, TVal3, TVal4> other) => other != null && EqualityComparer<GList<TKey>>.Default.Equals(Keys, other.Keys) && EqualityComparer<GList<TVal1>>.Default.Equals(Vals1, other.Vals1) && EqualityComparer<GList<TVal2>>.Default.Equals(Vals2, other.Vals2) && EqualityComparer<GList<TVal3>>.Default.Equals(Vals3, other.Vals3) && EqualityComparer<GList<TVal4>>.Default.Equals(Vals4, other.Vals4);

		public override int GetHashCode()
		{
			int hashCode = 165384537;
			hashCode = (hashCode * -1521134295) + EqualityComparer<GList<TKey>>.Default.GetHashCode(Keys);
			hashCode = (hashCode * -1521134295) + EqualityComparer<GList<TVal1>>.Default.GetHashCode(Vals1);
			hashCode = (hashCode * -1521134295) + EqualityComparer<GList<TVal2>>.Default.GetHashCode(Vals2);
			hashCode = (hashCode * -1521134295) + EqualityComparer<GList<TVal3>>.Default.GetHashCode(Vals3);
			hashCode = (hashCode * -1521134295) + EqualityComparer<GList<TVal4>>.Default.GetHashCode(Vals4);
			return hashCode;
		}

		public static bool operator ==(GDict<TKey, TVal1, TVal2, TVal3, TVal4> dict1, GDict<TKey, TVal1, TVal2, TVal3, TVal4> dict2) => EqualityComparer<GDict<TKey, TVal1, TVal2, TVal3, TVal4>>.Default.Equals(dict1, dict2);
		public static bool operator !=(GDict<TKey, TVal1, TVal2, TVal3, TVal4> dict1, GDict<TKey, TVal1, TVal2, TVal3, TVal4> dict2) => !(dict1 == dict2);
		#endregion

		#region Copy Ops
		public void CopyTo(out TVal1[] arrayVals) => arrayVals = Vals1.Count > 0 ? Vals1.Vals.ToArray() : (default);
		public void CopyTo(out List<TVal1> listVals) => listVals = Vals1.Vals ?? (default);
		public void CopyTo(out GList<TVal1> glistVals) => glistVals = new GList<TVal1>(Vals1.Vals);

		public void CopyTo(out TVal2[] arrayVals) => arrayVals = Vals2.Count > 0 ? Vals2.Vals.ToArray() : (default);
		public void CopyTo(out List<TVal2> listVals) => listVals = Vals2.Vals ?? (default);
		public void CopyTo(out GList<TVal2> glistVals) => glistVals = new GList<TVal2>(Vals2.Vals);

		public void CopyTo(out TVal3[] arrayVals) => arrayVals = Vals3.Count > 0 ? Vals3.Vals.ToArray() : (default);
		public void CopyTo(out List<TVal3> listVals) => listVals = Vals3.Vals ?? (default);
		public void CopyTo(out GList<TVal3> glistVals) => glistVals = new GList<TVal3>(Vals3.Vals);

		public void CopyTo(out TVal4[] arrayVals) => arrayVals = Vals4.Count > 0 ? Vals4.Vals.ToArray() : (default);
		public void CopyTo(out List<TVal4> listVals) => listVals = Vals4.Vals ?? (default);
		public void CopyTo(out GList<TVal4> glistVals) => glistVals = new GList<TVal4>(Vals4.Vals);

		public void CopyTo(out TKey[] arrayKeys) => arrayKeys = Keys.Count > 0 ? Keys.Vals.ToArray() : (default);
		public void CopyTo(out List<TKey> listKeys) => listKeys = Keys.Vals ?? (default);
		public void CopyTo(out GList<TKey> glistKeys) => glistKeys = new GList<TKey>(Keys.Vals);
		#endregion

		#region Enumeration
		public IEnumerator<GKeyValueSet<TKey, TVal1, TVal2, TVal3, TVal4>> GetEnumerator() => ((IEnumerable<GKeyValueSet<TKey, TVal1, TVal2, TVal3, TVal4>>)KeyValueSets).GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<GKeyValueSet<TKey, TVal1, TVal2, TVal3, TVal4>>)KeyValueSets).GetEnumerator();
		#endregion
	}
}