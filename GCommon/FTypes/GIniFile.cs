using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using GCommon.Collections;

namespace GCommon.FTypes
{
	public class GIniFile : IEquatable<GIniFile>, IEnumerable
	{
		#region Fields
		public string IniName { get; private set; }
		public FileInfo FileObj { get; set; }
		public GList<GIniSection> Sections { get; set; } = new GList<GIniSection>();

		public string Name => FileObj?.Name;
		public DirectoryInfo Dir => FileObj?.Directory;
		public bool Exists => FileObj != null && FileObj.Exists;
		public int Count => Sections.Count;
		public int LastIndex => Sections.Count - 1;
		public bool FileSaved => FileObj != null && SaveFile(GetLines());
		#endregion

		#region Constructors
		public GIniFile() { }
		public GIniFile(string iniName) => IniName = iniName;
		public GIniFile(string iniName, FileInfo fileObj) : this(iniName) => FileObj = fileObj;

		public static GIniFile Empty() => new GIniFile();
		#endregion

		#region Overrides
		/// <summary>Returns the name of the ini file.</summary>
		public override string ToString() => Name;

		/// <summary>Am I equal to the other object?</summary>
		public override bool Equals(object obj) => Equals(obj as GIniFile);
		#endregion

		#region Equalities
		/// <summary>Is this object equal to the provided other object?</summary>
		public bool Equals(GIniFile other) => other != null && IniName == other.IniName && EqualityComparer<FileInfo>.Default.Equals(FileObj, other.FileObj) && EqualityComparer<GList<GIniSection>>.Default.Equals(Sections, other.Sections);

		/// <summary>Returns a hash of the current object.</summary>
		public override int GetHashCode()
		{
			int hashCode = -782268093;
			hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(IniName);
			hashCode = (hashCode * -1521134295) + EqualityComparer<FileInfo>.Default.GetHashCode(FileObj);
			hashCode = (hashCode * -1521134295) + EqualityComparer<GList<GIniSection>>.Default.GetHashCode(Sections);
			return hashCode;
		}

		/// <summary>Am I equal to the other object?</summary>
		public static bool operator ==(GIniFile file1, GIniFile file2) => EqualityComparer<GIniFile>.Default.Equals(file1, file2);

		/// <summary>Am I not equal to the other object?</summary>
		public static bool operator !=(GIniFile file1, GIniFile file2) => !(file1 == file2);
		#endregion

		#region FileOps
		/// <summary>Initiates saving the file to disk.</summary>
		public void PrepForSave()
		{
			if (FileObj != null)
				SaveFile(GetLines());
		}

		/// <summary>Gets the lines to save.</summary>
		GList<string> GetLines()
		{
			GList<string> Lines = new GList<string>();

			foreach (GIniSection sect in Sections)
			{
				Lines.Add(sect.ToString());
				int pairsIndexMax = sect.Pairs.Count - 1;

				foreach (GIniValuePair pair in sect.Pairs)
				{
					Lines.Add(pair.ToString());

					if (sect.Pairs.IndexOf(pair) == pairsIndexMax)
						Lines.Add(string.Empty);
				}
			}

			return Lines;
		}

		/// <summary>Writes the file to disk.</summary>
		public bool SaveFile(GList<string> Lines)
		{
			if (Lines != null && Lines.Count > 0)
			{
				if (Exists)
					FileObj.Delete();

				File.WriteAllLines(FileObj.FullName, Lines.ToArray());
				FileObj.Refresh();
				return Exists;
			}

			return false;
		}
		#endregion

		#region Special
		/// <summary>Returns True if the section name has been found to exist.</summary>
		public bool Contains(string sectionName)
		{
			foreach (GIniSection sect in Sections)
			{
				if (sect.SectName == sectionName)
					return true;
			}

			return false;
		}

		/// <summary>Returns the index of the given section name, or -1 if not available.</summary>
		public int IndexOf(string sectionName)
		{
			foreach (GIniSection sect in Sections)
			{
				if (sect.SectName == sectionName)
					return Sections.IndexOf(sect);
			}

			return -1;
		}

		/// <summary>Gets or sets the value based on the given section name.</summary>
		public GIniSection this[string sectionName]
		{
			get
			{
				foreach (GIniSection sect in Sections)
				{
					if (sect.SectName == sectionName)
						return sect;
				}

				return null;
			}
		}

		/// <summary>Gets or sets the value based on the given index.</summary>
		public GIniSection this[int index]
		{
			get => LastIndex >= index ? Sections[index] : null;
			set
			{
				if (LastIndex <= index)
					Sections[index] = value;
				else
					Sections.Add(value);
			}
		}
		#endregion

		#region Enumeration
		public IEnumerator GetEnumerator() => ((IEnumerable)Sections).GetEnumerator();
		#endregion
	}
}