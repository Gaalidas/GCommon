using System.Collections.Generic;
using System.IO;
using GCommon.Collections;

namespace GCommon.IO
{
	public static class GDirOps
	{
		#region Directory Name Manipulation
		public static bool RenameDir(this DirectoryInfo sourceDir, string newName)
		{
			string newPath = $"{sourceDir.Parent.FullName.GetValidPath()}{newName}";

			if (sourceDir.Exists && !Directory.Exists(newPath))
				sourceDir.MoveTo(newPath);

			return Directory.Exists(newPath);
		}

		public static string CleanFileName(this FileInfo inputFile) => inputFile.Name.Replace(inputFile.Extension, string.Empty).TrimEnd('.');
		public static string CleanFileExt(this FileInfo inputFile) => inputFile.Extension.TrimStart('.');
		#endregion

		#region GetDirs
		public static List<DirectoryInfo> GetDirs(this DirectoryInfo root, bool tryRecursive = false) => root.TryGetDirs(out List<DirectoryInfo> output, tryRecursive) ? output : (default);
		public static List<DirectoryInfo> GetDirs(this DirectoryInfo root, string nameTarget, bool tryRecursive = false) => root.TryGetDirs(nameTarget, out List<DirectoryInfo> output, tryRecursive) ? output : (default);

		public static bool TryGetDirs(this DirectoryInfo root, out List<DirectoryInfo> output, bool tryRecursive = false)
		{
			output = (default);

			if (!root.Exists)
				return false;

			foreach (DirectoryInfo dir in root.GetDirectories("*", tryRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
				output.Add(dir);

			return output.Count != 0;
		}

		public static bool TryGetDirs(this DirectoryInfo root, string nameTarget, out List<DirectoryInfo> output, bool tryRecursive = false)
		{
			output = (default);

			if (!root.Exists)
				return false;

			foreach (DirectoryInfo dir in root.GetDirectories(nameTarget, tryRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
				output.Add(dir);

			return output.Count != 0;
		}

		public static bool TryGetDirFirst(this List<DirectoryInfo> input, string nameTarget, out DirectoryInfo output)
		{
			output = null;

			foreach (DirectoryInfo dir in input)
			{
				if (dir.Name.Contains(nameTarget))
				{
					output = dir;
					break;
				}
			}

			return output != null;
		}

		public static bool TryGetDirFirst(this GList<DirectoryInfo> input, string nameTarget, out DirectoryInfo output)
		{
			output = null;

			foreach (DirectoryInfo dir in input)
			{
				if (dir.Name.Contains(nameTarget))
				{
					output = dir;
					break;
				}
			}

			return output != null;
		}
		#endregion
	}
}