using System;
using System.Collections;
using System.Collections.Generic;
using GCommon.Collections;

namespace GCommon.GYaml
{
	/// <summary>Represents a single line of data which can have multiple Key/Value pairs in it.</summary>
	public class GYamlDataCollection : IEnumerable<GYamlDataPair>, IEquatable<GYamlDataCollection>
	{
		/// <summary>This collection's parent node.</summary>
		public GYamlNode Parent { get; set; }

		/// <summary>A list of all pairs found on this line.</summary>
		public GList<GYamlDataPair> DataPairs { get; set; } = GList.Empty<GYamlDataPair>();

		/// <summary>The original line passed to this colelction.</summary>
		public string Line { get; set; }

		/// <summary>Gets or sets the value for the given key, if the key matches an already-existing pair.  Otherwise, adds a new pair with the given key and value.</summary>
		public string this[string key]
		{
			get
			{
				foreach (GYamlDataPair pair in DataPairs)
					return pair[key];

				return string.Empty;
			}
			set
			{
				foreach (GYamlDataPair pair in DataPairs)
				{
					if (pair.Key.ToLower() == key.ToLower())
					{
						pair[key] = value;
						return;
					}
				}

				DataPairs.Add(new GYamlDataPair(key, value));
			}
		}

		public GYamlDataCollection(GYamlNode parent)
		{
			DataPairs.Add(new GYamlDataPair("name", string.Empty));
			DataPairs.Add(new GYamlDataPair("id", "0"));

			if (Line.IsNull())
				Line = string.Empty;

			Parent = parent;
		}

		public GYamlDataCollection(string line, GYamlNode parent) : this(parent) => Line = line;

		/// <summary>Checks to see if the given key exists in any <see cref="GYamlDataPair"/> objects contained in this collection.</summary>
		public bool Contains(string key)
		{
			foreach (GYamlDataPair pair in DataPairs)
			{
				if (!pair[key].IsNullOrEmpty())
					return true;
			}

			return false;
		}

		public bool Add(string key, string value)
		{
			if (value.IsNullOrEmpty())
				return false;

			foreach (GYamlDataPair pair in DataPairs)
			{
				if (pair.Key.ToLower() == key.ToLower())
					return false;
			}

			int dataPairCount = DataPairs.Count;
			DataPairs.Add(new GYamlDataPair(key, value));
			return dataPairCount < DataPairs.Count;
		}

		public bool IsValid => !Line.IsNullOrEmpty() && DataPairs.Count > 0;

		public override bool Equals(object obj) => Equals(obj as GYamlDataCollection);
		public bool Equals(GYamlDataCollection other) => other != null && EqualityComparer<GList<GYamlDataPair>>.Default.Equals(DataPairs, other.DataPairs);

		public IEnumerator<GYamlDataPair> GetEnumerator() => ((IEnumerable<GYamlDataPair>)DataPairs).GetEnumerator();
		public override int GetHashCode() => -565418762 + EqualityComparer<GList<GYamlDataPair>>.Default.GetHashCode(DataPairs);

		IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<GYamlDataPair>)DataPairs).GetEnumerator();

		public static bool operator ==(GYamlDataCollection collection1, GYamlDataCollection collection2) => EqualityComparer<GYamlDataCollection>.Default.Equals(collection1, collection2);
		public static bool operator !=(GYamlDataCollection collection1, GYamlDataCollection collection2) => !(collection1 == collection2);
	}
}