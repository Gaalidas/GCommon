using System.Collections.Generic;
using System.IO;
using GCommon.Collections;

namespace GCommon.IO
{
	public static class GFileOps
	{
		#region Deletion
		public static bool DeleteFile(this FileInfo fileTarget)
		{
			if (fileTarget.Exists)
				fileTarget.Delete();

			return !fileTarget.Exists;
		}
		#endregion

		#region Get Files
		public static bool GetFilesByPattern(this DirectoryInfo root, string filePattern, out List<FileInfo> output, bool tryRecursive = true)
		{
			output = (default);

			foreach (FileInfo file in root.GetFiles(filePattern, tryRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
				output.Add(file);

			return output.Count != 0;
		}

		public static bool GetFilesByPattern(this DirectoryInfo root, string filePattern, out GList<FileInfo> output, bool tryRecursive = true)
		{
			output = (default);

			foreach (FileInfo file in root.GetFiles(filePattern, tryRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
				output.Add(file);

			return output.Count != 0;
		}

		public static bool GetFileFirstByName(this List<FileInfo> input, string nameTarget, out FileInfo output)
		{
			output = null;

			foreach (FileInfo file in input)
			{
				if (file.Name.Contains(nameTarget) || file.Extension.Contains(nameTarget))
				{
					output = file;
					break;
				}
			}

			return output != null;
		}

		public static bool GetFileFirstByName(this GList<FileInfo> input, string nameTarget, out FileInfo output)
		{
			output = null;

			foreach (FileInfo file in input)
			{
				if (file.Name.Contains(nameTarget) || file.Extension.Contains(nameTarget))
				{
					output = file;
					break;
				}
			}

			return output != null;
		}
		#endregion
	}
}