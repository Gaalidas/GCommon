using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using GCommon.Xml;

namespace GCommon.FTypes
{
	public class GXmlFile : IEquatable<GXmlFile>
	{
		#region Fields
		public string XmlName { get; private set; }
		public FileInfo FileObj { get; private set; }
		public XmlDocument XmlDoc { get; private set; } = new XmlDocument();
		public bool IsLoaded => FileObj == null ? false : XmlDoc.HasChildNodes;
		public bool Exists => FileObj != null && FileObj.Exists;
		public string Name => FileObj?.Name;
		public DirectoryInfo Dir => FileObj?.Directory;
		#endregion

		#region Constructor
		public GXmlFile(string xmlName, FileInfo fileObj)
		{
			XmlName = xmlName;
			FileObj = fileObj;
			LoadXmlDoc();
		}
		#endregion

		#region Loader
		public void LoadXmlDoc()
		{
			if (FileObj != null && Exists && !IsLoaded)
				XmlDoc.Load(FileObj.FullName);
		}
		#endregion

		#region File Ops
		public bool DeleteFile()
		{
			if (FileObj != null && Exists)
				FileObj.Delete();

			return !FileObj.Exists;
		}

		public bool SaveFile()
		{
			if (FileObj != null && DeleteFile() && IsLoaded)
				XmlDoc.Save(FileObj.FullName);

			FileObj.Refresh();
			return Exists;
		}
		#endregion

		#region Overrides
		public override string ToString() => Name;
		public override bool Equals(object obj) => Equals(obj as GXmlFile);

		public override int GetHashCode()
		{
			int hashCode = 130442366;
			hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(XmlName);
			hashCode = (hashCode * -1521134295) + EqualityComparer<FileInfo>.Default.GetHashCode(FileObj);
			hashCode = (hashCode * -1521134295) + EqualityComparer<XmlDocument>.Default.GetHashCode(XmlDoc);
			return hashCode;
		}
		#endregion

		#region Equality Comparisons
		public bool Equals(GXmlFile other) => other != null && XmlName == other.XmlName && EqualityComparer<FileInfo>.Default.Equals(FileObj, other.FileObj) && EqualityComparer<XmlDocument>.Default.Equals(XmlDoc, other.XmlDoc);
		public static bool operator ==(GXmlFile file1, GXmlFile file2) => EqualityComparer<GXmlFile>.Default.Equals(file1, file2);
		public static bool operator !=(GXmlFile file1, GXmlFile file2) => !(file1 == file2);
		#endregion

		#region Special Ops
		public XmlNodeList this[string nodeName] => IsLoaded ? XmlDoc.GetElementsByTagName(nodeName) : null;

		public bool HasNodeByName(string nodeName) => IsLoaded && XmlDoc.GetNodesByName(nodeName, out XmlNodeList nodeList) && nodeList.Count > 0;
		#endregion
	}
}