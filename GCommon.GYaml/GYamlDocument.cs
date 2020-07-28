using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using GCommon.Collections;
using GCommon.GYaml.Enums;

namespace GCommon.GYaml
{
	public class GYamlDocument : IEnumerable<string>, IEquatable<GYamlDocument>
	{
		public GYamlFile Parent { get; private set; }
		public GList<string> Lines { get; set; } = GList.Empty<string>();
		public GYamlNode Top { get; set; } = new GYamlNode(GYamlNodeType.TopLayer);

		public GYamlDocument(GYamlFile parent)
		{
			Top.DataCollections.Add(new GYamlDataCollection(Top));
			Top.DataCollections[0].Add("name", "Top");
			Top.Lines = Lines;
		}

		public void Load(string filePath) => InternalLoad(filePath);
		public void Load(FileInfo fileInput) => InternalLoad(fileInput.FullName);
		public void Load(GYamlFile gYaml) => InternalLoad(gYaml.FileObj.FullName);

		public bool IsLoaded => Top.Lines.Count > 0;
		public bool IsValid => IsLoaded;

		void InternalLoad(string filePath) => Lines = File.ReadAllLines(filePath).ToGList();

		public IEnumerator<string> GetEnumerator() => ((IEnumerable<string>)Lines).GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<string>)Lines).GetEnumerator();

		public override bool Equals(object obj) => Equals(obj as GYamlDocument);
		public bool Equals(GYamlDocument other) => other != null && EqualityComparer<GYamlFile>.Default.Equals(Parent, other.Parent) && EqualityComparer<GList<string>>.Default.Equals(Lines, other.Lines) && EqualityComparer<GYamlNode>.Default.Equals(Top, other.Top);

		public override int GetHashCode()
		{
			int hashCode = 133480837;
			hashCode = (hashCode * -1521134295) + EqualityComparer<GYamlFile>.Default.GetHashCode(Parent);
			hashCode = (hashCode * -1521134295) + EqualityComparer<GList<string>>.Default.GetHashCode(Lines);
			hashCode = (hashCode * -1521134295) + EqualityComparer<GYamlNode>.Default.GetHashCode(Top);
			return hashCode;
		}

		public static bool operator ==(GYamlDocument document1, GYamlDocument document2) => EqualityComparer<GYamlDocument>.Default.Equals(document1, document2);
		public static bool operator !=(GYamlDocument document1, GYamlDocument document2) => !(document1 == document2);
	}
}