using System;
using System.Collections.Generic;
using System.IO;
using GCommon.Collections;
using GCommon.Math;

namespace GCommon.FTypes
{
	public class GTxtFile : IEquatable<GTxtFile>
	{
		#region Fields
		/// <summary>ID name of this instance.</summary>
		public string TxtName { get; private set; }

		/// <summary><see cref="FileInfo"/> object associated with this instance.</summary>
		public FileInfo FileObj { get; private set; }

		/// <summary>The name of the file on the drive.</summary>
		public string Name => FileObj?.Name;

		/// <summary>The <see cref="DirectoryInfo"/> object representing it's container directory.</summary>
		public DirectoryInfo Dir => FileObj?.Directory;

		/// <summary>Does the file exist on the drive?</summary>
		public bool Exists => FileObj != null && FileObj.Exists;

		/// <summary>Contains all lines in the file.</summary>
		public GList<string> Lines { get; private set; } = new GList<string>();

		/// <summary>Try to save file.. did it work?</summary>
		public bool FileSaved => FileObj != null && SaveFile();
		#endregion

		#region Constructors
		public GTxtFile(string txtName) => TxtName = txtName;
		public GTxtFile(string txtName, FileInfo file) : this(txtName) => FileObj = file;
		#endregion

		#region FileOps
		public bool DeleteFile()
		{
			if (FileObj.Exists)
				FileObj.Delete();

			return !FileObj.Exists;
		}

		public bool SaveFile()
		{
			if (Exists && Lines != null && Lines.Count > 0)
			{
				DeleteFile();
				File.WriteAllLines(FileObj.FullName, Lines.ToArray());
				FileObj.Refresh();
				return Exists;
			}

			return false;
		}
		#endregion

		#region Overrides
		public override string ToString() => FileObj?.FullName;
		public override bool Equals(object obj) => Equals(obj as GTxtFile);
		#endregion

		#region Comparisons
		public bool Equals(GTxtFile other) => other != null && TxtName == other.TxtName && EqualityComparer<FileInfo>.Default.Equals(FileObj, other.FileObj) && EqualityComparer<GList<string>>.Default.Equals(Lines, other.Lines);

		public override int GetHashCode()
		{
			int hashCode = 1874820644;
			hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(TxtName);
			hashCode = (hashCode * -1521134295) + EqualityComparer<FileInfo>.Default.GetHashCode(FileObj);
			hashCode = (hashCode * -1521134295) + EqualityComparer<GList<string>>.Default.GetHashCode(Lines);
			return hashCode;
		}

		public static bool operator ==(GTxtFile file1, GTxtFile file2) => EqualityComparer<GTxtFile>.Default.Equals(file1, file2);
		public static bool operator !=(GTxtFile file1, GTxtFile file2) => !(file1 == file2);
		#endregion

		#region Data Manipulation
		public void AddLine(string message)
		{
			if (Lines != null)
				Lines.Add(message);
		}

		public void RemoveLine(int lineIndex)
		{
			if (Lines != null)
			{
				lineIndex = lineIndex.Clamp(0, Lines.Count - 1);
				Lines.RemoveAt(lineIndex);
			}
		}

		public bool ReplaceLine(int lineIndex, string newLine)
		{
			if (Lines != null && Lines.Count > 0)
			{
				lineIndex = lineIndex.Clamp(0, Lines.Count - 1);
				Lines[lineIndex] = newLine;
				return Lines[lineIndex] == newLine;
			}

			return false;
		}
		#endregion

		#region Special
		public bool HasLine(string line)
		{
			foreach (string lineObj in Lines)
			{
				if (line == lineObj)
					return true;
			}

			return false;
		}
		#endregion
	}
}