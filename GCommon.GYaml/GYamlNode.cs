using System;
using System.Collections;
using System.Collections.Generic;
using GCommon.Collections;
using GCommon.GYaml.Enums;

namespace GCommon.GYaml
{
	public class GYamlNode : IEquatable<GYamlNode>, IEnumerable<GYamlDataCollection>, IEnumerable<string>
	{
		public GList<string> Lines { get; set; } = GList.Empty<string>();
		public GYamlNodeType NodeType { get; set; } = GYamlNodeType.Unknown;
		public GList<GYamlDataCollection> DataCollections { get; set; } = GList.Empty<GYamlDataCollection>();

		public int NodeID
		{
			get
			{
				foreach (GYamlDataCollection datacol in DataCollections)
				{
					if (int.TryParse(datacol["id"], out int outInt))
						return outInt;
				}

				return 0;
			}
			set
			{
				foreach (GYamlDataCollection datacol in DataCollections)
				{
					if (datacol.Contains("id"))
						datacol["id"] = value.ToString();
				}
			}
		}

		public string NodeName
		{
			get
			{
				foreach (GYamlDataCollection datacol in DataCollections)
				{
					if (!datacol["name"].IsNullOrEmpty())
						return datacol["name"];
				}

				return string.Empty;
			}
			set
			{
				foreach (GYamlDataCollection datacol in DataCollections)
				{
					if (datacol.Contains("name"))
						datacol["name"] = value;
				}
			}
		}

		public GYamlNode(GYamlNodeType nodeType) => NodeType = nodeType;
		public GYamlNode(GList<string> lines, GYamlNodeType nodeType) : this(nodeType) => Lines = lines;

		public override bool Equals(object obj) => Equals(obj as GYamlNode);
		public bool Equals(GYamlNode other) => other != null && EqualityComparer<GList<string>>.Default.Equals(Lines, other.Lines) && NodeType == other.NodeType;

		public override int GetHashCode()
		{
			int hashCode = 1551488833;
			hashCode = (hashCode * -1521134295) + EqualityComparer<GList<string>>.Default.GetHashCode(Lines);
			hashCode = (hashCode * -1521134295) + NodeType.GetHashCode();
			return hashCode;
		}

		public IEnumerator<GYamlDataCollection> GetEnumerator() => ((IEnumerable<GYamlDataCollection>)DataCollections).GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<GYamlDataCollection>)DataCollections).GetEnumerator();
		IEnumerator<string> IEnumerable<string>.GetEnumerator() => ((IEnumerable<string>)Lines).GetEnumerator();

		public static bool operator ==(GYamlNode node1, GYamlNode node2) => EqualityComparer<GYamlNode>.Default.Equals(node1, node2);
		public static bool operator !=(GYamlNode node1, GYamlNode node2) => !(node1 == node2);
	}
}