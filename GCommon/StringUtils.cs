using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GCommon
{
	/// <summary>The most basic and low-level utility methods.</summary>
	public static class StringUtils
	{
		#region String Manipulation
		/// <summary>Extension method for string.IsNullOrEmpty(input)</summary>
		public static bool IsNullOrEmpty(this string input) => string.IsNullOrEmpty(input);

		/// <summary>Returns true if the input is null, not caring if's empty even if empty is implied..</summary>
		public static bool IsNull(this string input) => input == null;

		/// <summary>Returns true if the input is empty but not null.</summary>
		public static bool IsEmpty(this string input) => input == string.Empty;

		/// <summary>Simply makes sure that the input string path has the ending backslash in it.</summary>
		public static void GetValidPath(this string input, out string output) => output = !input.EndsWith(@"\") ? $"{input}\\" : input;

		/// <summary>Simply makes sure that the input string path has the ending backslash in it.</summary>
		public static string GetValidPath(this string input)
		{
			input.GetValidPath(out string output);
			return output;
		}

		/// <summary>Extracts the file name from the input path if one exists.</summary>
		/// <param name="input">the input string.</param>
		/// <param name="output">the results.</param>
		/// <returns>True if the removal was successful.</returns>
		public static bool RemoveFileName(this string input, out string output)
		{
			output = string.Empty;
			List<string> inputList = input.Contains(".") ? input.Split('.').ToList() : (default);

			if (inputList.Count > 0)
			{
				int lastindex = inputList.GetLastIndex();
				inputList.RemoveRange(0, lastindex - 1);
				output = inputList[0];
			}

			return !output.IsNullOrEmpty();
		}

		/// <summary>Extracts the file extension from the input path if one exists.</summary>
		/// <param name="input">the input string.</param>
		/// <param name="output">the results.</param>
		/// <returns>True if the removal was successful.</returns>
		public static bool RemoveFileExt(this string input, out string output)
		{
			output = string.Empty;
			List<string> inputArray = input.Contains(".") ? input.Split('.').ToList() : (default);

			if (inputArray.Count > 0 && inputArray.RemoveLastIndex(out IList newInputArray))
			{
				newInputArray = (List<string>)newInputArray;
				output = newInputArray.Count > 1 ? string.Join(".", newInputArray) : newInputArray[0].ToString();
			}

			return output != string.Empty;
		}

		/// <summary>Attempts to remove the directory portion from the file name.</summary>
		public static bool RemoveDirFromFullName(this DirectoryInfo input, out string output)
		{
			string inputPath = input.FullName.GetValidPath();
			string inputParentPath = input.Parent.FullName.GetValidPath();
			output = inputPath.Replace(inputParentPath, string.Empty);
			output = output.EndsWith("\\") ? output.TrimEnd("\\".ToCharArray()) : output;
			return !output.IsNullOrEmpty();
		}

		/// <summary>Adaptation of a method found privatized and unreferenced in the <see cref="DirectoryInfo"/> class.</summary>
		public static string GetDirName(this DirectoryInfo dir)
		{
			string fullPath = dir.FullName;

			if (fullPath.Length > 3)
			{
				if (fullPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
					fullPath = fullPath.Substring(0, fullPath.Length - 1);

				return Path.GetFileName(fullPath);
			}

			return fullPath;
		}

		/// <summary>Gets the directory name, outputs it, and returns true if it's not the same as the input full path.</summary>
		public static bool GetDirName(this DirectoryInfo dir, out string output) => (output = dir.GetDirName()) != dir.FullName;
		#endregion

		#region Case Conversions
		public static string[] ToLower(this string[] input)
		{
			for (int i = 0; i < input.Length; i++)
				input[i] = input[i].ToLower();

			return input;
		}

		public static string[] ToUpper(this string[] input)
		{
			for (int i = 0; i < input.Length; i++)
				input[i] = input[i].ToUpper();

			return input;
		}
		#endregion

		#region Starts And Ends
		public static bool StartsWith(this string input, char findChr) => input.StartsWith(findChr.ToString());
		public static bool EndsWith(this string input, char findChr) => input.EndsWith(findChr.ToString());
		#endregion
	}
}