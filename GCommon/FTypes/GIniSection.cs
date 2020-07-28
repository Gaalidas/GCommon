using System;
using System.Collections;
using System.Collections.Generic;
using GCommon.Collections;

namespace GCommon.FTypes
{
	public class GIniSection : IEquatable<GIniSection>, IEnumerable
	{
		#region Fields
		public string SectName { get; set; }
		public GList<GIniValuePair> Pairs { get; set; } = new GList<GIniValuePair>();
		public GIniFile Parent { get; private set; }
		#endregion

		#region Constructor
		public GIniSection(string sectName, GIniFile parent)
		{
			SectName = sectName;
			Parent = parent;
		}
		#endregion

		#region Equality
		public override string ToString() => $"[{SectName}]";
		public override bool Equals(object obj) => Equals(obj as GIniSection);
		public bool Equals(GIniSection other) => other != null && SectName == other.SectName && EqualityComparer<GList<GIniValuePair>>.Default.Equals(Pairs, other.Pairs);

		public override int GetHashCode()
		{
			int hashCode = 1404488700;
			hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(SectName);
			hashCode = (hashCode * -1521134295) + EqualityComparer<GList<GIniValuePair>>.Default.GetHashCode(Pairs);
			return hashCode;
		}

		public static bool operator ==(GIniSection section1, GIniSection section2) => EqualityComparer<GIniSection>.Default.Equals(section1, section2);
		public static bool operator !=(GIniSection section1, GIniSection section2) => !(section1 == section2);
		#endregion

		#region Ops
		/// <summary>Number of pairs in this section.</summary>
		public int Count => Pairs.Count;

		/// <summary>Total length, in lines, this section covers including the section header.</summary>
		public int Length => Pairs.Count + 1;

		/// <summary>Returns the last index in the list of pairs.</summary>
		public int LastIndex => Count - 1;

		public bool Contains(string key)
		{
			foreach (GIniValuePair pair in Pairs)
			{
				if (pair.Key == key)
					return true;
			}

			return false;
		}

		public bool Contains(string key, out int index)
		{
			index = -1;

			foreach (GIniValuePair pair in Pairs)
			{
				if (pair.Key == key)
				{
					index = Pairs.IndexOf(pair);
					break;
				}
			}

			return index != -1;
		}

		/// <summary>Returns the index of the supplied key, or -1 if the key is not found.</summary>
		public int IndexOf(string key) => Contains(key, out int keyIndex) ? keyIndex : -1;
		public IEnumerator GetEnumerator() => ((IEnumerable)Pairs).GetEnumerator();

		public string this[string key]
		{
			get => Contains(key, out int keyIndex) ? Pairs[keyIndex].Vals.Count > 1 ? string.Join(", ", Pairs[keyIndex].Vals) : Pairs[keyIndex].Vals[0] : (default);
			set
			{
				if (Contains(key, out int keyIndex))
				{
					GList<string> newValue = new GList<string>();

					if (value.Contains(","))
					{
						string[] values = value.Split(',');

						for (int i = 0; i < values.Length; i++)
							values[i].Trim(' ');

						newValue.AddRange(values);
						Pairs[keyIndex].Vals = newValue;
					}
					else
					{
						newValue.Add(value);
						Pairs[keyIndex].Vals = newValue;
					}
				}
				else
					Pairs.Add(new GIniValuePair(key, value, this));
			}
		}

		public string this[int index] => index <= LastIndex ? Pairs[index].Vals.Count > 1 ? string.Join(", ", Pairs[index].Vals) : Pairs[index].Vals[0] : (default);
		#endregion
	}
}