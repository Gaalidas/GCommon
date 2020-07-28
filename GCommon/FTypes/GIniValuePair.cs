using System;
using System.Collections;
using System.Collections.Generic;
using GCommon.Collections;

namespace GCommon.FTypes
{
	public class GIniValuePair : IEquatable<GIniValuePair>, IEnumerable
	{
		#region Fields
		public string Key { get; private set; }
		public GList<string> Vals { get; set; } = new GList<string>();
		public GIniSection Parent { get; private set; }
		#endregion

		#region Constructors
		public GIniValuePair(string key, GIniSection parent)
		{
			Key = key;
			Parent = parent;
		}

		public GIniValuePair(string key, string val, GIniSection parent) : this(key, parent) => Vals = val.Split(',').ToGList();
		public GIniValuePair(string key, string[] vals, GIniSection parent) : this(key, parent) => Vals = new GList<string>(vals);

		public static GIniValuePair Empty(string key, GIniSection parent) => new GIniValuePair(key, parent);
		#endregion

		#region Ops
		public string this[string item]
		{
			get => Key == item ? Vals.Count > 1 ? string.Join(", ", Vals) : Vals[0] : string.Empty;
			set
			{
				if (Vals.Contains(item))
					Vals[Vals.IndexOf(item)] = value;
				else if (value.Contains(","))
				{
					Vals.Clear();
					Vals.AddRange(value.Split(", ".ToCharArray()));
				}
				else if (item.IsNullOrEmpty())
					Vals[0] = value;
				else
					Vals.Add(item);
			}
		}

		public override string ToString() => Vals.Count > 1 ? $"{Key}={string.Join(", ", Vals)}" : $"{Key}={Vals[0]}";

		public override bool Equals(object obj) => Equals(obj as GIniValuePair);
		public bool Equals(GIniValuePair other) => other != null && Key == other.Key && Vals == other.Vals && EqualityComparer<GIniSection>.Default.Equals(Parent, other.Parent);

		public override int GetHashCode()
		{
			int hashCode = 1335136357;
			hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Key);
			hashCode = (hashCode * -1521134295) + EqualityComparer<GList<string>>.Default.GetHashCode(Vals);
			hashCode = (hashCode * -1521134295) + EqualityComparer<GIniSection>.Default.GetHashCode(Parent);
			return hashCode;
		}

		public IEnumerator GetEnumerator() => ((IEnumerable)Vals).GetEnumerator();

		public static bool operator ==(GIniValuePair pair1, GIniValuePair pair2) => EqualityComparer<GIniValuePair>.Default.Equals(pair1, pair2);
		public static bool operator !=(GIniValuePair pair1, GIniValuePair pair2) => !(pair1 == pair2);
		#endregion
	}
}