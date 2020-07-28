using System.IO;
using System.Xml;
using GCommon.Collections;
using GCommon.Enums;
using GCommon.FTypes;
using GCommon.IO;

namespace GCommon.Xml
{
	public static class GXmlUtils
	{
		#region Get XML Files
		public static bool GetXmlFiles(this DirectoryInfo root, string fileNamePattern, out GList<FileInfo> output, bool tryRecursive = false)
		{
			fileNamePattern = !fileNamePattern.IsNullOrEmpty() ? $"{fileNamePattern.Replace("*", string.Empty).Replace(".xml", string.Empty)}*.xml" : "*.xml";
			return root.GetFilesByPattern(fileNamePattern, out output);
		}
		#endregion

		#region Node Gathering GXmlFiles
		public static bool GetNodesByName(this GXmlFile fileTarget, string nodeName, out XmlNodeList output)
		{
			output = null;

			if (!fileTarget.IsLoaded)
				fileTarget.LoadXmlDoc();

			output = fileTarget.XmlDoc.GetElementsByTagName(nodeName);
			return output != null && output.Count > 0;
		}

		public static bool GetFirstNodeByName(this GXmlFile fileTarget, string nodeName, out XmlNode output)
		{
			output = null;

			if (!fileTarget.IsLoaded)
				fileTarget.LoadXmlDoc();

			output = fileTarget.XmlDoc.GetElementsByTagName(nodeName)[0];
			return output != null;
		}
		#endregion

		#region Get Files As GXmlFile
		public static bool GetGXmlFiles(this DirectoryInfo root, out GList<GXmlFile> output, string fileNamePattern = "", bool tryRecursive = true)
		{
			output = (default);

			if (root.GetXmlFiles(fileNamePattern, out GList<FileInfo> fileList, tryRecursive))
			{
				foreach (FileInfo file in fileList)
					output.Add(new GXmlFile(file.Name, file));
			}

			return output.Count > 0;
		}
		#endregion
	}
}