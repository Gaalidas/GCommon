using System;
using GCommon.Enums;

namespace GCommon
{
	public static class ParseUtils
	{
		#region TryParse
		/// <summary>Returns true if the input is an integer.</summary>
		public static bool TryParseInt(this string input) => int.TryParse(input, out int output);

		/// <summary>Returns true if the input is a double.</summary>
		public static bool TryParseDouble(this string input) => double.TryParse(input, out double output);

		/// <summary>Returns true if the input is a float.</summary>
		public static bool TryParseFloat(this string input) => float.TryParse(input, out float output);

		/// <summary>Returns true if the input is a boolean.</summary>
		public static bool TryParseBool(this string input) => bool.TryParse(input, out bool output);

		/// <summary>Generic method to try and parse the input based on a supplied data type.</summary>
		public static bool TryParse(this string input, DataType checkType = DataType.Int)
		{
			switch (checkType)
			{
				case DataType.Int:
					return input.TryParseInt();
				case DataType.Double:
					return input.TryParseDouble();
				case DataType.Float:
					return input.TryParseFloat();
				case DataType.Bool:
					return input.TryParseBool();
				default:
					return false;
			}
		}

		public static bool TryParseInt(this char input) => int.TryParse(input.ToString(), out int outInt);
		public static bool TryParseDouble(this char input) => double.TryParse(input.ToString(), out double outDbl);
		public static bool TryParseFloat(this char input) => float.TryParse(input.ToString(), out float outFlt);

		public static bool TryParseInt(this char[] input) => int.TryParse(input.ToString(), out int outInt);
		public static bool TryParseDouble(this char[] input) => double.TryParse(input.ToString(), out double outDbl);
		public static bool TryParseFloat(this char[] input) => float.TryParse(input.ToString(), out float outFlt);
		#endregion

		#region Find And Remove
		/// <summary>Finds and removes the supplies search <see cref="string"/> from the supplied <see cref="string"/>.</summary>
		/// <param name="input">The <see cref="string"/> to search in.</param>
		/// <param name="output">The resulting <see cref="string"/>.</param>
		public static bool Remove(this string input, string findStr, out string output)
		{
			output = input;
			string findStrLower = findStr.ToLower();
			string inputLower = input.ToLower();

			if (inputLower.Contains(findStrLower))
				output = output.Replace(findStr, string.Empty);

			return input != output;
		}

		/// <summary>Removes the first instance of the supplied <see cref="string"/> from the subject <see cref="string"/> and returns true if successful.</summary>
		/// <param name="input">The <see cref="string"/> to search in.</param>
		/// <param name="findStr">The <see cref="string"/> to search for.</param>
		/// <param name="output">The resulting <see cref="string"/>.</param>
		public static bool RemoveFirst(this string input, string findStr, out string output)
		{
			output = input;
			string inputLower = input.ToLower();
			string findStrLower = findStr.ToLower();
			int indexOfFirst = inputLower.IndexOf(findStrLower);

			if (indexOfFirst == 0)
				output = input.Substring(findStr.Length);
			else if (indexOfFirst > 0)
				output = $"{input.Substring(0, indexOfFirst)}{input.Substring(indexOfFirst + findStr.Length)}";

			return output != input;
		}

		/// <summary>Removes the last instance of the supplied <see cref="string"/> from the subject <see cref="string"/> and returns true if successful.</summary>
		/// <param name="input">The <see cref="string"/> to search in.</param>
		/// <param name="findStr">The <see cref="string"/> to search for.</param>
		/// <param name="output">The resulting <see cref="string"/>.</param>
		public static bool RemoveLast(this string input, string FindStr, out string output)
		{
			output = input;
			string inputLower = input.ToLower();
			string findStrLower = FindStr.ToLower();
			int indexOfLast = inputLower.LastIndexOf(findStrLower);

			if (indexOfLast == 0)
				output = input.Substring(FindStr.Length);
			else if (indexOfLast > 0)
				output = $"{input.Substring(0, indexOfLast)}{input.Substring(indexOfLast + FindStr.Length)}";

			return output != input;
		}

		/// <summary>Removes all occurences of the supplied <see cref="Array"/>(<see cref="char"/>) from the input and returns true if successful.</summary>
		/// <param name="input">The <see cref="Array"/>(<see cref="char"/>) to search in.</param>
		/// <param name="findChars">The <see cref="Array"/>(<see cref="char"/>) to search for.</param>
		/// <param name="output">The resulting <see cref="Array"/>(<see cref="char"/>).</param>
		public static bool Remove(this char[] input, char[] findChars, out char[] output)
		{
			output = input;

			if (input.ToString().Remove(findChars.ToString(), out string outStr))
				output = outStr.ToCharArray();

			return output != input;
		}

		/// <summary>Removes the first occurence of the supplied <see cref="Array"/>(<see cref="char"/>) from the input and returns true if successful.</summary>
		/// <param name="input">The <see cref="Array"/>(<see cref="char"/>) to search in.</param>
		/// <param name="findChars">The <see cref="Array"/>(<see cref="char"/>) to search for.</param>
		/// <param name="output">The resulting <see cref="Array"/>(<see cref="char"/>).</param>
		public static bool RemoveFirst(this char[] input, char[] findChars, out char[] output)
		{
			output = input;

			if (input.ToString().RemoveFirst(findChars.ToString(), out string outStr))
				output = outStr.ToCharArray();

			return output != input;
		}

		/// <summary>Removes the last occurence of the supplied <see cref="Array"/>(<see cref="char"/>) from the input and returns true if successful.</summary>
		/// <param name="input">The <see cref="Array"/>(<see cref="char"/>) to search in.</param>
		/// <param name="findChars">The <see cref="Array"/>(<see cref="char"/>) to search for.</param>
		/// <param name="output">The resulting <see cref="Array"/>(<see cref="char"/>).</param>
		public static bool RemoveLast(this char[] input, char[] findChars, out char[] output)
		{
			output = input;

			if (input.ToString().RemoveLast(findChars.ToString(), out string outStr))
				output = outStr.ToCharArray();

			return output != input;
		}

		/// <summary>Removes the all occurences of the supplied <see cref="string"/> from the input and returns true if successful.</summary>
		/// <param name="input">The <see cref="Array"/>(<see cref="string"/>) to search in.</param>
		/// <param name="findChars">The <see cref="string"/> to search for.</param>
		/// <param name="output">The resulting <see cref="Array"/>(<see cref="string"/>).</param>
		public static bool Remove(this string[] input, string findStr, out string[] output)
		{
			output = input;

			for (int i = 0; i < input.Length; i++)
			{
				if (output[i].Remove(findStr, out string outStr))
					output[i] = outStr;
			}

			return output != input;
		}

		/// <summary>Removes the first occurence of the supplied <see cref="string"/> from the input and returns true if successful.</summary>
		/// <param name="input">The <see cref="Array"/>(<see cref="string"/>) to search in.</param>
		/// <param name="findChars">The <see cref="string"/> to search for.</param>
		/// <param name="output">The resulting <see cref="Array"/>(<see cref="string"/>).</param>
		public static bool RemoveFirst(this string[] input, string FindStr, out string[] output)
		{
			output = input;

			for (int i = 0; i < input.Length; i++)
			{
				if (output[i].RemoveFirst(FindStr, out string outStr))
					output[i] = outStr;
			}

			return output != input;
		}

		/// <summary>Removes the last occurence of the supplied <see cref="string"/> from the input and returns true if successful.</summary>
		/// <param name="input">The <see cref="Array"/>(<see cref="string"/>) to search in.</param>
		/// <param name="findChars">The <see cref="string"/> to search for.</param>
		/// <param name="output">The resulting <see cref="Array"/>(<see cref="string"/>).</param>
		public static bool RemoveLast(this string[] input, string FindStr, out string[] output)
		{
			output = input;

			for (int i = 0; i < input.Length; i++)
			{
				if (output[i].RemoveLast(FindStr, out string outStr))
					output[i] = outStr;
			}

			return output != input;
		}
		#endregion
	}
}