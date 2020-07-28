using System.IO;

namespace GCommon.GYaml
{
	public class GYamlFile
	{
		public FileInfo FileObj { get; private set; }

		public DirectoryInfo Dir => FileObj?.Directory;
		public string Name => FileObj?.Name;
		public bool Exists => FileObj.Exists;
		public GYamlDocument YamlDoc { get; set; }

		public GYamlFile(FileInfo fileObj)
		{
			FileObj = fileObj;
			YamlDoc = new GYamlDocument(this);

			if (Exists)
				YamlDoc.Load(fileObj);

			if (YamlDoc.IsLoaded)
				YamlDoc.TryParse();
		}
	}
}