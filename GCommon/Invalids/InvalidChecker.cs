using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GCommon.IO;

namespace GCommon.Invalids
{
	public static class InvalidChecker
	{
		static readonly string[] InvalidStringPatterns = new string[] { "con", "prn", "aux", "nul", "com1", "com2", "com3", "com4", "com5", "com6", "com7", "com8", "com9", "com0", "lpt1", "lpt2", "lpt3", "lpt4", "lpt5", "lpt6", "lpt7", "lpt8", "lpt9", "lpt0" };
		static readonly char[] InvalidCharPatterns = new char[] { '<', '>', ':', '\"', '/', '\\', '|', '?', '*' };
		static readonly List<string> InvalidDirNameStrings = new List<string>() { ":", "v1.3", "1.0", "2.0", "1.1", "R1.1", "V5.0", "[]", "[ ]", " - ", "  ", "   ", "    ", "     " };
		static readonly string InvalidDirNameReplace = " ";

		public static bool HasInvalidName(this DirectoryInfo input)
		{
			string inputStr = input.FullName.EndsWith("\\") ? input.FullName.TrimEnd("\\".ToCharArray()) : input.FullName;
			string parentDirStr = !input.Parent.FullName.EndsWith("\\") ? $"{input.Parent.FullName}\\" : input.Parent.FullName;
			inputStr = inputStr.Contains(parentDirStr) ? inputStr.Replace(parentDirStr, string.Empty) : inputStr;
			return inputStr.HasInvalidName();
		}

		public static bool HasInvalidName(this FileInfo input)
		{
			string[] fileNameArray = input.Name.Split('.');
			string inputStr = fileNameArray.Length > 0 ? fileNameArray[0] : string.Empty;
			return fileNameArray.Length == 0 || fileNameArray.Length > 2 ? false : inputStr.HasInvalidName();
		}

		public static bool HasInvalidName(this string input)
		{
			foreach (string invalid in InvalidStringPatterns)
			{
				if (input.ToLower() == invalid)
					return true;
			}

			return false;
		}

		public static string ReplaceInvalid(this string input)
		{
			string[] inputArray1 = Array.Empty<string>();
			string[] inputArray2 = Array.Empty<string>();
			string inputStr = string.Empty;
			string output = string.Empty;
			bool[] containsitem = new bool[2];
			int[] arrayLastIndecis = new int[2];

			if (input.Contains("\\"))
			{
				containsitem[0] = true;
				inputArray1 = input.Split("\\".ToCharArray());
				arrayLastIndecis[0] = inputArray1.Length > 0 ? inputArray1.Length - 1 : -1;

				if (arrayLastIndecis[0] != -1)
					inputArray1[arrayLastIndecis[0]] = inputArray1[arrayLastIndecis[0]].Replace("\\", string.Empty);
			}

			if (containsitem[0] && arrayLastIndecis[0] != -1 && inputArray1[arrayLastIndecis[0]].Contains("."))
			{
				containsitem[1] = true;
				inputArray2 = inputArray1[arrayLastIndecis[0]].Split('.');
			}
			else if (!containsitem[0] && input.Contains("."))
			{
				containsitem[1] = true;
				inputArray2 = input.Split('.');
			}

			arrayLastIndecis[1] = inputArray2.Length >= 2 ? inputArray2.Length - 2 : inputArray2.Length == 1 ? inputArray2.Length - 1 : -1;

			if ((containsitem[0] && containsitem[1]) || (!containsitem[0] && containsitem[1]))
				inputArray2[arrayLastIndecis[1]] = inputArray2[arrayLastIndecis[1]].RemoveInvalid();
			else if (containsitem[0] && !containsitem[1])
				inputArray1[arrayLastIndecis[0]] = inputArray1[arrayLastIndecis[0]].RemoveInvalid();
			else if (!containsitem[0] && !containsitem[1])
				return input;

			if (containsitem[0] && containsitem[1])
			{
				inputArray1[arrayLastIndecis[0]] = string.Join(".", inputArray2);
				output = string.Join("\\", inputArray1);
			}
			else if (containsitem[0] && !containsitem[1])
				output = string.Join("\\", inputArray1);
			else if (!containsitem[0] && containsitem[1])
				output = string.Join(".", inputArray2);

			return output;
		}

		public static string RemoveInvalidsFromPath(this string input)
		{
			if (!input.Contains("\\"))
				return input.RemoveInvalids();

			input = input.EndsWith("\\") ? input.TrimEnd("\\".ToCharArray()) : input;
			List<string> inputs = input.Split("\\".ToCharArray()).ToList();
			string output = inputs[inputs.Count - 1];
			inputs.RemoveAt(inputs.Count - 1);
			string outputJoined = string.Join("\\", inputs);
			output = output.RemoveInvalids();
			return $"{outputJoined}\\{output}";
		}

		public static void RemoveInvalidsFromPath(this DirectoryInfo input, out string output) => output = input.FullName.RemoveInvalidsFromPath();
		public static void RemoveInvalidsFromPath(this DirectoryInfo input, out DirectoryInfo output) => output = new DirectoryInfo(input.FullName.RemoveInvalidsFromPath());

		public static string RemoveInvalids(this string input)
		{
			string output = input.TrimEnd('\\');

			for (int i = 0; i < 2; i++)
			{
				foreach (string invalidStr in InvalidDirNameStrings)
				{
					if (output.Contains(invalidStr))
						output = output.Replace(invalidStr, InvalidDirNameReplace);
				}
			}

			while (output.StartsWith("[ ]") || output.StartsWith("[]") || output.EndsWith("[ ]") || output.EndsWith("[]"))
				output = output.Replace("[ ]", string.Empty).Replace("[]", string.Empty);

			while (output.StartsWith(" ") || output.EndsWith(" "))
				output = output.Trim(' ');

			return output;
		}

		public static string RemoveInvalidsFromFile(this FileInfo file) => $"{file.CleanFileName().RemoveInvalids()}.{file.CleanFileExt().RemoveInvalids()}";

		static string RemoveInvalid(this string input)
		{
			string output = input;

			foreach (string invalid in InvalidStringPatterns)
			{
				if (output.ToLower() == invalid)
				{
					output = $"{output}_1";
					break;
				}
			}

			foreach (char invalidchr in InvalidCharPatterns)
			{
				if (output.Contains(invalidchr.ToString()))
					output = output.Replace(invalidchr.ToString(), string.Empty);
			}

			return output;
		}
	}
}