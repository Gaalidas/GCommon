using System;
using System.Collections;
using System.IO;
using GCommon.Collections;
using GCommon.Enums;

namespace GCommon
{
	public static class DataTypeUtils
	{
		#region DataType Determination
		/// <summary>Determines <see cref="DataType"/> and returns true of type is not unknown. Outputs the type enum object.</summary>
		public static bool DetermineDataType(this object input, out DataType output)
		{
			output = DataType.Unknown;

			switch (input)
			{
				case int _:
					output = DataType.Int;
					break;
				case double _:
					output = DataType.Double;
					break;
				case float _:
					output = DataType.Float;
					break;
				case string _:
					output = DataType.String;
					break;
				case char _:
					output = DataType.Char;
					break;
				case FileInfo _:
					output = DataType.FileInfo;
					break;
				case DirectoryInfo _:
					output = DataType.DirectoryInfo;
					break;
				case Array _:
				{
					DataType arrayType = DataType.Array;

					switch (input)
					{
						case int[] _:
							arrayType = DataType.ArrayInt;
							break;
						case double[] _:
							arrayType = DataType.ArrayDouble;
							break;
						case float[] _:
							arrayType = DataType.ArrayFloat;
							break;
						case string[] _:
							arrayType = DataType.ArrayString;
							break;
						case char[] _:
							arrayType = DataType.ArrayChar;
							break;
						case FileInfo[] _:
							arrayType = DataType.ArrayFileInfo;
							break;
						case DirectoryInfo[] _:
							arrayType = DataType.ArrayDirectoryInfo;
							break;
						default:
							break;
					}

					output = arrayType;
					break;
				}
				case IList _:
				{
					DataType listType = DataType.IList;

					switch (input)
					{
						case GList<int> _:
							listType = DataType.ListInt;
							break;
						case GList<double> _:
							listType = DataType.ListDouble;
							break;
						case GList<float> _:
							listType = DataType.ListFloat;
							break;
						case GList<char> _:
							listType = DataType.ListChar;
							break;
						case GList<string> _:
							listType = DataType.ListString;
							break;
						case GList<FileInfo> _:
							listType = DataType.ListFileInfo;
							break;
						case GList<DirectoryInfo> _:
							listType = DataType.ListDirectoryInfo;
							break;
						default:
							break;
					}

					output = listType;
					break;
				}
				case GList _:
				{
					DataType glistType = DataType.GList;

					switch (input)
					{
						case GList<int> _:
							glistType = DataType.GListInt;
							break;
						case GList<double> _:
							glistType = DataType.GListDouble;
							break;
						case GList<float> _:
							glistType = DataType.GListFloat;
							break;
						case GList<char> _:
							glistType = DataType.GListChar;
							break;
						case GList<string> _:
							glistType = DataType.GListString;
							break;
						case GList<FileInfo> _:
							glistType = DataType.GListFileInfo;
							break;
						case GList<DirectoryInfo> _:
							glistType = DataType.GListDirectoryInfo;
							break;
						default:
							break;
					}

					output = glistType;
					break;
				}
				default:
					break;
			}

			return output != DataType.Unknown;
		}

		/// <summary>Returns true of the object is an array of any kind.</summary>
		public static bool IsArray(this object input)
		{
			if (input.DetermineDataType(out DataType outType) && outType != DataType.Unknown)
			{
				switch (outType)
				{
					case DataType.Int:
					case DataType.Double:
					case DataType.Float:
					case DataType.String:
					case DataType.Char:
					case DataType.FileInfo:
					case DataType.DirectoryInfo:
					case DataType.GIniFile:
					case DataType.GArgObject:
					case DataType.GXmlFile:
					case DataType.GTxtFile:
					case DataType.Bool:
						return false;
					default:
						return true;
				}
			}

			return false;
		}

		/// <summary>Simply returns true if the input is a number that can be parsed by a double.</summary>
		public static bool IsNumeric(this string input) => input.TryParseInt() || input.TryParseDouble() || input.TryParseFloat();

		/// <summary>Returns true if the input can be parsed to an integer and outputs the integer.</summary>
		public static bool IsNumeric(this string input, out int output) => int.TryParse(input, out output);

		/// <summary>Returns true if the input can be parsed to a double and outputs the double.</summary>
		public static bool IsNumeric(this string input, out double output) => double.TryParse(input, out output);

		/// <summary>Returns true if the input can be parsed to a float and outputs the float.</summary>
		public static bool IsNumeric(this string input, out float output) => float.TryParse(input, out output);
		#endregion
	}
}